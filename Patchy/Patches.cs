using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using SevenZip;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Patchy
{
    public class Patches
    {

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        public BindingList<string> List;
        public static string PatchyFolder = AppDomain.CurrentDomain.BaseDirectory + "Patchy\\";
        public static string TempFolder = PatchyFolder + "Temp\\";

        public Dictionary<string, PatchInfo> patches;

        public Patches()
        {
            List = new BindingList<string>();
            patches = new Dictionary<string, PatchInfo>();

            if(Directory.Exists(PatchyFolder + "db\\"))
            {
                string[] fdbs = Directory.GetFiles(PatchyFolder + "db\\", "*.fdb", SearchOption.TopDirectoryOnly);
                foreach (string file in fdbs)
                {
                    //PatchInfo pi = ReadFromBinaryFile<PatchInfo>(file);
                    //patches.Add(pi.name, pi);
                    string name = new FileInfo(file).Name.Replace(".fdb", "");
                    List.Add(name);
                }
            }
            ReverseList();
        }

        public void Add(string file)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (object sender, DoWorkEventArgs e) => {
                Log.A(false);
                if (Directory.Exists(TempFolder))
                {
                    Directory.Delete(TempFolder, true);
                }
                Directory.CreateDirectory(TempFolder);

                if (File.Exists(file))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        FileInfo archiveInfo = new FileInfo(file);

                        Log.W("");
                        Log.W("Peeking into \"" + archiveInfo.Name + "\" to see if there is a valid patch in there..");
                        SevenZipExtractor.SetLibraryPath("lib/7z.dll");
                        SevenZipExtractor extractor = new SevenZipExtractor(fs);
                        extractor.Extracting += (object pSender, ProgressEventArgs pe) => {
                            Log.P(pe.PercentDone);
                        };

                        if (extractor.ArchiveFileNames.Contains("FirefallInstaller_md5") && extractor.ArchiveFileNames.Contains("FirefallInstaller.7z") && extractor.ArchiveFileNames.Contains("FirefallInstaller.ver"))
                        {
                            string patch = String.Empty;
                            DateTime date = new DateTime();
                            bool hasDate = false;
                            string goodHash;

                            foreach (ArchiveFileInfo adata in extractor.ArchiveFileData)
                            {
                                if(adata.FileName.Equals("FirefallInstaller.7z"))
                                {
                                    hasDate = true;
                                    date = adata.LastWriteTime;
                                    break;
                                }
                            }
                            
                            using (BinaryReader vStream = new BinaryReader(new MemoryStream()))
                            {
                                extractor.ExtractFile("FirefallInstaller.ver", vStream.BaseStream);
                                vStream.BaseStream.Seek(0, SeekOrigin.Begin);
                                patch = Encoding.ASCII.GetString(vStream.ReadBytes((int)vStream.BaseStream.Length)).Replace("\r", "").Replace("\n", "").Trim();
                            }
                            Console.WriteLine(patch);

                            using (BinaryReader vStream = new BinaryReader(new MemoryStream()))
                            {
                                extractor.ExtractFile("FirefallInstaller_md5", vStream.BaseStream);
                                vStream.BaseStream.Seek(0, SeekOrigin.Begin);
                                goodHash = Encoding.ASCII.GetString(vStream.ReadBytes((int)vStream.BaseStream.Length)).Replace("\r", "").Replace("\n", "").Trim();
                            }

                            // workaround for missing revision numbers
                            if(patch.Equals("beta-1478"))
                            {
                                if(goodHash.Equals("84b2dd8fa98960a0ae6004f7b7ff1552"))
                                {
                                    patch += "1";
                                }
                                else if (goodHash.Equals("29d52cf143a70fa153a155eb61cd50ef"))
                                {
                                    patch += "2";
                                }
                            }
                            else if (patch.Equals("beta-1524") && goodHash.Equals("0dbfa467a4ec154d320459501c455d85"))
                            {
                                patch += "1";
                            }

                            string[] pSplit = patch.Split(new[] {'-'});
                            if(pSplit.Length > 1 && pSplit[1].Length == 4)
                            {
                                patch += "0";
                            }

                            Console.WriteLine(patch);
                            if (!String.IsNullOrEmpty(patch))
                            {
                                if (!Has(patch))
                                {
                                    if (IsLatestPatch(patch))
                                    {
                                        Log.W("Patch \""+patch.Replace("\n", "")+"\" found. Released at: " + ((hasDate) ? date.ToShortDateString() + " " + date.ToShortTimeString() : " no idea") + "..");
                                        Log.W("Extracting " + Math.Round((double)extractor.PackedSize / 1073741824.0d, 2) + "GB (compressed) to a temporary dir!");
                                        extractor.ExtractFiles(TempFolder, extractor.ArchiveFileNames.ToArray<string>());
                                        
                                        string file2 = TempFolder + "FirefallInstaller.7z";
                                        string hashFile = TempFolder + "FirefallInstaller_md5";
                                        
                                        if(File.Exists(file2) && File.Exists(hashFile))
                                        {
                                            byte[] vBuffer;
                                            byte[] vOldBuffer;
                                            int vBytesRead;
                                            int vOldBytesRead;
                                            long vSize;
                                            long vTotalBytesRead = 0;

                                            using (FileStream fs2 = new FileStream(file2, FileMode.Open))
                                            using (HashAlgorithm md5 = MD5.Create())
                                            {
                                                Log.W("Done extracting.. Verifying integrity of files..");

                                                vSize = fs2.Length;
                                                vBuffer = new byte[4096*8];
                                                vBytesRead = fs2.Read(vBuffer, 0, vBuffer.Length);
                                                vTotalBytesRead += vBytesRead;

                                                do
                                                {
                                                    vOldBytesRead = vBytesRead;
                                                    vOldBuffer = vBuffer;

                                                    vBuffer = new byte[4096*8];
                                                    vBytesRead = fs2.Read(vBuffer, 0, vBuffer.Length);

                                                    vTotalBytesRead += vBytesRead;
   
                                                    if (vBytesRead == 0)
                                                    {
                                                        md5.TransformFinalBlock(vOldBuffer, 0, vOldBytesRead);
                                                    }
                                                    else
                                                    {
                                                        md5.TransformBlock(vOldBuffer, 0, vOldBytesRead, vOldBuffer, 0);
                                                    }

                                                    Log.P((int)((double)vTotalBytesRead * 100 / vSize));
                                                } while (vBytesRead != 0);


                                                string hash = BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();

                                                if(goodHash.Length > 0)
                                                {
                                                    if(goodHash.Equals(hash))
                                                    {
                                                        Log.W("File verification was a success!");
                                                        Tuple<string, int> latestPatch = MostRecentPatch();
                                                        string parentPatch = (latestPatch != null ? latestPatch.Item1 : null);

                                                        if (Directory.Exists(PatchyFolder + "\\" + patch))
                                                        {
                                                            Directory.Delete(PatchyFolder + "\\" + patch, true);
                                                        }
                                                        Directory.CreateDirectory(PatchyFolder + "\\" + patch);

                                                        PatchInfo patchInfo = new PatchInfo(patch, parentPatch, date, hasDate);

                                                        fs2.Seek(0, SeekOrigin.Begin);
                                                        extractor.Dispose();

                                                        extractor = new SevenZipExtractor(fs2);
                                                        bool eSuccess = false;

                                                        if(!String.IsNullOrEmpty(parentPatch))
                                                        {
                                                            LoadToMemory(parentPatch);
                                                            if (patches.ContainsKey(parentPatch))
                                                            {
                                                                Log.W("Comparing " + patch + " (new) with " + parentPatch + " (previous) patch.");
                                                                Log.W("Extracting new or changed files, creating symlinks for unaffected files to save space: Patchy\\" + patch);

                                                                foreach (ArchiveFileInfo data in extractor.ArchiveFileData)
                                                                {
                                                                    if (!data.IsDirectory)
                                                                    {
                                                                        PatchInfo.PFile f = new PatchInfo.PFile();
                                                                        f.file = data.FileName;
                                                                        f.size = data.Size;
                                                                        f.crc = data.Crc;
                                                                        patchInfo.Add(data.FileName, f);

                                                                        if (patches[parentPatch].files.ContainsKey(data.FileName))
                                                                        {
                                                                            if (data.Crc != patches[parentPatch].files[data.FileName].crc)
                                                                            {
                                                                                f.oldFile = data.FileName;
                                                                                patchInfo.modifiedFiles.Add(f.file);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            patchInfo.newFiles.Add(f.file);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Directory.CreateDirectory(PatchyFolder + "\\" + patch + "\\" + data.FileName);
                                                                    }
                                                                }

                                                                foreach (KeyValuePair<string, PatchInfo.PFile> pf in patches[parentPatch].files)
                                                                {
                                                                    if(!patchInfo.files.ContainsKey(pf.Value.file))
                                                                    {
                                                                        patchInfo.deletedFiles.Add(pf.Value.file);
                                                                    }
                                                                }

                                                                extractor.Extracting += (object pSender, ProgressEventArgs pe) =>
                                                                {
                                                                    Log.P(pe.PercentDone);
                                                                };

                                                                extractor.ExtractFiles(PatchyFolder + "\\" + patch, patchInfo.modifiedFiles.ToArray().Union(patchInfo.newFiles.ToArray()).ToArray());
                                                                foreach (KeyValuePair<string, PatchInfo.PFile> pf in patchInfo.files)
                                                                {
                                                                    
                                                                    if (!patchInfo.newFiles.Contains(pf.Value.file) && !patchInfo.modifiedFiles.Contains(pf.Value.file))
                                                                    {
                                                                        string nFile = PatchyFolder + patch + "\\" + pf.Value.file;
                                                                        string oFile = PatchyFolder + parentPatch + "\\" + pf.Value.file;
                                                                        CreateSymbolicLink(nFile, oFile, 0);
                                                                    } 
                                                                }
                                                                eSuccess = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            extractor.Extracting += (object pSender, ProgressEventArgs pe) =>
                                                            {
                                                                Log.P(pe.PercentDone);
                                                            };
                                                            Log.W("This is the first patch you are adding, extracting all files to: Patchy\\" + patch);
                                                            extractor.ExtractFiles(PatchyFolder + "\\" + patch, extractor.ArchiveFileNames.ToArray<string>());
                                                            
                                                            foreach (ArchiveFileInfo adata in extractor.ArchiveFileData)
                                                            {
                                                                if(!adata.IsDirectory)
                                                                {
                                                                    PatchInfo.PFile f = new PatchInfo.PFile();
                                                                    f.file = adata.FileName;
                                                                    f.size = adata.Size;
                                                                    f.crc = adata.Crc;
                                                                    patchInfo.newFiles.Add(adata.FileName);
                                                                    patchInfo.Add(adata.FileName, f);
                                                                }
                                                            }
                                                            eSuccess = true;
                                                        }

                                                        if(eSuccess)
                                                        {
                                                            Log.W("Extraction done! Saving database and cleaning up! :D");
                                                            Directory.CreateDirectory(PatchyFolder + "\\db");
                                                            WriteToBinaryFile<PatchInfo>(PatchyFolder + "\\db\\" + patch + ".fdb", patchInfo);

                                                            patches.Add(patch, patchInfo);
                                                            ReverseList();
                                                            List.Add(patch);
                                                            ReverseList();
                                                            Log.RefreshPatchList();
                                                        }
                                                        else
                                                        {
                                                            Log.W("Extraction failed! Something terrible happened :<");
                                                        }

                                                    }
                                                    else
                                                    {
                                                        Log.W("File verification returned an error, files may be corrupt, aborting.. :C");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Log.W("Not exactly sure what happened, but extraction didnt work.. aborting :C");
                                        }
                                    }
                                    else
                                    {
                                        Log.W("We already added a newer patch than this, we cant add it without redoing everything after the provided patch soo.. aborting :C");
                                    }
                                }
                                else
                                {
                                    Log.W("We already did this patch, aborting.. :C");
                                }
                            }
                            else
                            {
                                Log.W("Did not find the files we were looking for, aborting.. :C");
                            }

                        }

                    }
                }

                if (Directory.Exists(TempFolder))
                {
                    Directory.Delete(TempFolder, true);
                }
                Log.A(true);
            };
            bw.RunWorkerAsync();
        }

        public bool Has(string patch)
        {
            return List.Contains(patch);
        }

        public bool IsLatestPatch(string patch)
        {
            int n = PatchStrAsInt(patch);
            foreach(string s in List)
            {
                if (PatchStrAsInt(s) >= n)
                {
                    return false;
                }
            }
            return true;
        }

        public Tuple<string, int> MostRecentPatch()
        {
            if(List.Count > 0)
            {
                string[] p = List.ToArray<string>();
                int[] pi = new int[p.Length];

                for (int i = 0; i < p.Length; i++)
                {
                    pi[i] = PatchStrAsInt(p[i]);
                }

                int indexAtMax = pi.ToList().IndexOf(pi.Max());
                return new Tuple<string, int>(p[indexAtMax], pi[indexAtMax]);
            }
            return null;
        }

        public void LoadToMemory(string name)
        {
            if(!patches.ContainsKey(name))
            {
                PatchInfo pi = ReadFromBinaryFile<PatchInfo>(PatchyFolder +"\\db\\" + name + ".fdb");
                patches.Add(pi.name, pi);
            }
        }

        public int PatchStrAsInt(string patch)
        {
            string[] s = patch.Replace("\n", "").Split(new string[]{"-"}, StringSplitOptions.None);
            int o = 0;
            if(s.Length == 2)
            {
                if (s[1].Length == 4)
                {
                    s[1] += "0";
                }
                int.TryParse(s[1], out o);        
            }
            return o;
        }

        public void ReverseList()
        {
            List<string> sl = List.ToList<string>();
            sl.Reverse();
            List.Clear();
            foreach (string s in sl)
            {
                List.Add(s);
            }
        }
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            using (Stream gZipStream = new GZipStream(stream, System.IO.Compression.CompressionMode.Compress))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(gZipStream, objectToWrite);
            }
        }
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            using (Stream gZipStream = new GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(gZipStream);
            }
        }


        [Serializable]
        public class PatchInfo
        {
            public DateTime creationTime;
            public string name;
            public string parentName;
            public Dictionary<string, PFile> files;
            public List<string> newFiles;
            public List<string> modifiedFiles;
            public List<string> deletedFiles;

            public PatchInfo(string name, string parentName, DateTime creationTime, bool hasDate)
            {
                this.name = name;
                this.parentName = parentName;
                if (hasDate)
                {
                    this.creationTime = creationTime;
                }

                this.files = new Dictionary<string, PFile>();
                this.newFiles = new List<string>();
                this.modifiedFiles = new List<string>();
                this.deletedFiles = new List<string>();
            }

            public void Add(string file, PFile pFile)
            {
                files.Add(file, pFile);
            }

            [Serializable]
            public class PFile
            {
                public string file = "";
                public ulong size = 0;
                public uint crc = 0;
                public string oldFile = "";
            }
        }
    }
}
