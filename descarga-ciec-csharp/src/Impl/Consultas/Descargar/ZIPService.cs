using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Interfaces;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace descarga_ciec_sdk.src.Impl.Consultas.Descargar
{
    public class ZIPService : IZIPService
    {
        /// <summary>
        ///
        /// </summary>
        private const string EXTENSION_ZIP = ".zip";

        /// <summary>
        ///
        /// </summary>
        public ZIPService() { }

        /// <summary>
        ///
        /// </summary>
        public string DescargarYDescomprimirZIPAsync_(
            string uri,
            string nombreZIP,
            string path,
            string token = null
        )
        {
            string tempPathFull = "";

            string tempPathFullConExtensionZiP = "";
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("La URI de descarga es requerida");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

              
                string pathFull = path;
                string pathFullConExtensionZip = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                tempPathFullConExtensionZiP = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");
                tempPathFull = Path.Combine(path.ToString(), nombreZIP);

                DescargarZipAsync(uri, tempPathFullConExtensionZiP, token).Wait();
                DescomprimirAsync(tempPathFullConExtensionZiP, tempPathFull).Wait();

            }
            catch (Exception)
            {
                throw;
            }
            return tempPathFullConExtensionZiP;
        }

        /// <summary>
        ///
        /// </summary>
        public string DescargarZIP(string uri, string nombreZIP, string path)
        {
            string tempPathFull = "";

            string tempPathFullConExtensionZiP = "";
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("La URI de descarga es requerida");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                string pathTemp = Path.Combine(path, "TEMP");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (!Directory.Exists(pathTemp))
                {
                    Directory.CreateDirectory(pathTemp);
                }

                string pathFull = path;
                string pathFullConExtensionZip = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                tempPathFullConExtensionZiP = Path.Combine(pathTemp, $"{nombreZIP}{EXTENSION_ZIP}");
                tempPathFull = Path.Combine(pathTemp.ToString(), nombreZIP);

                DescargarZipAsync(uri, tempPathFullConExtensionZiP).Wait();
                //  descomprimirAsync(tempPathFullConExtensionZiP, tempPathFull).Wait();


                //  var listaXMLs =  extraerTodosArchivosDirectorioXMLList(tempPathFull, pathFull);
            }
            catch (Exception)
            {
                throw;
            }
            return tempPathFullConExtensionZiP;
        }

        /// <summary>
        ///
        /// </summary>
        public async Task GuardarZIPFileBase64YDescomprimirZIPAsync(
            byte[] paqueteFromBase64String,
            string nombreZIP,
            string path
        )
        {
            try
            {
                if (paqueteFromBase64String == null)
                {
                    throw new ArgumentNullException("El paquete  es requerido");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                string pathTemp = Path.Combine(path, "XXX");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (!Directory.Exists(pathTemp))
                {
                    Directory.CreateDirectory(pathTemp);
                }

                string pathFull = path;
                string pathFullConExtensionZip = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                string tempPathFullConExtensionZiP = Path.Combine(
                    pathTemp,
                    $"{nombreZIP}{EXTENSION_ZIP}"
                );
                string tempPathFull = Path.Combine(pathTemp.ToString(), nombreZIP);

                //Guardamos el archivo en base64 en el disco.
                await GuardarZIPFileBase64Async(
                    tempPathFullConExtensionZiP,
                    paqueteFromBase64String
                );

                //Obtenemos la información del ZIP
                await DescomprimirAsync(tempPathFullConExtensionZiP, tempPathFull);

                // Para el caso del TXT tenemos que solo extrar el txt


                var listaXMLs = ExtraerTodosArchivosDirectorioXMLListAsync(tempPathFull, pathFull);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void DescargarZIP(string uri, string nombreZIP, string path, string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("La URI de descarga es requerida");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP es requerido");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string pathFull = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                DescargarZip(uri, pathFull, token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="filename"></param>
        public void DescargarZip(string uri, string pathFull, string token)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/zip");

                    client.DownloadFile(new Uri(uri), @pathFull);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public async Task DescargarZIPAsync(
            string uri,
            string nombreZIP,
            string path,
            string token = null
        )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("La URI de descarga es requerida");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string pathFull = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                await DescargarZipAsync(uri, pathFull, token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void GuardarZIPFileBase64(
            byte[] paqueteFromBase64String,
            string nombreZIP,
            string path
        )
        {
            try
            {
                if (paqueteFromBase64String == null)
                {
                    throw new ArgumentNullException("El paquete  es requerido");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string pathFull = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                GuardarZIPFileBase64(pathFull, paqueteFromBase64String);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public async Task GuardarZIPFileBase64Async(
            byte[] paqueteFromBase64String,
            string nombreZIP,
            string path
        )
        {
            try
            {
                if (paqueteFromBase64String == null)
                {
                    throw new ArgumentNullException("El paquete  es requerido");
                }

                if (string.IsNullOrWhiteSpace(nombreZIP))
                {
                    throw new ArgumentNullException("El nombre del ZIP  es requerida");
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("El path del ZIP  es requerido");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string pathFull = Path.Combine(path, $"{nombreZIP}{EXTENSION_ZIP}");

                await GuardarZIPFileBase64Async(pathFull, paqueteFromBase64String);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="filename"></param>
        public async Task DescargarZipAsync(string uri, string pathFull, string token = null)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/zip");

                    if (token != null)
                    {
                        client.Headers.Add(HttpRequestHeader.Authorization, $"{token}");
                    }

                    await client.DownloadFileTaskAsync(new Uri(uri), @pathFull);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paqueteFromBase64String"></param>
        public void GuardarZIPFileBase64(string @path, byte[] paqueteFromBase64String)
        {
            try
            {
                using (System.IO.FileStream fs = File.Create(@path, paqueteFromBase64String.Length))
                {
                    fs.Write(paqueteFromBase64String, 0, paqueteFromBase64String.Length);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paqueteFromBase64String"></param>
        public async Task GuardarZIPFileBase64Async(string @path, byte[] paqueteFromBase64String)
        {
            try
            {
                using (
                    System.IO.FileStream fs = await Task.Run(
                        () => File.Create(@path, paqueteFromBase64String.Length)
                    )
                )
                {
                    await fs.WriteAsync(paqueteFromBase64String, 0, paqueteFromBase64String.Length);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool Descomprimir(string pathZIP, string pathSalida)
        {
            bool isOk = false;
            try
            {
                if (File.Exists(pathZIP))
                {
                    if (pathZIP.Contains(".zip") || pathZIP.Contains(".zip"))
                    {
                        System.IO.Directory.CreateDirectory(pathSalida);

                        using (Stream stream = File.OpenRead(pathZIP))
                        {
                            var reader = ReaderFactory.Open(stream);
                            reader.WriteAllToDirectory(
                                pathSalida,
                                new ExtractionOptions() { ExtractFullPath = true, Overwrite = true }
                            );
                        }
                    }
                    else
                    {
                        using (
                            System.IO.Compression.ZipArchive zipArchive = ZipFile.Open(
                                pathZIP,
                                ZipArchiveMode.Read
                            )
                        )
                        {
                            zipArchive.ExtractToDirectory(pathSalida);
                        }
                    }
                }

                isOk = true;
            }
            catch (Exception)
            {
                throw;
            }

            return isOk;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DescomprimirAsync(string pathZIP, string pathSalida)
        {
            bool isOk = false;
            try
            {
                if (File.Exists(pathZIP))
                {
                    if (pathZIP.Contains(".zip") || pathZIP.Contains(".zip"))
                    {
                        System.IO.Directory.CreateDirectory(pathSalida);

                        using (Stream stream = File.OpenRead(pathZIP))
                        {
                            var reader = ReaderFactory.Open(stream);
                            await Task.Run(
                                () =>
                                    reader.WriteAllToDirectory(
                                        pathSalida,
                                        new ExtractionOptions()
                                        {
                                            ExtractFullPath = true,
                                            Overwrite = true,
                                        }
                                    )
                            );
                        }
                    }
                    else
                    {
                        using (
                            System.IO.Compression.ZipArchive zipArchive = ZipFile.Open(
                                pathZIP,
                                ZipArchiveMode.Read
                            )
                        )
                        {
                            await Task.Run(() => zipArchive.ExtractToDirectory(pathSalida));
                        }
                    }
                }

                isOk = true;
            }
            catch (Exception)
            {
                throw;
            }

            return isOk;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pathEntrada"></param>
        /// <param name="pathSalida"></param>
        /// <param name="patron"></param>
        /// <returns></returns>
        public void ExtraerTodosArchivosDirectorioAsync(string pathEntrada, string pathSalida)
        {
            var archivos = Directory.GetFiles(pathEntrada, "XML", SearchOption.AllDirectories);
            string nombreArchivo = "";
            string pathArchivo = "";

            foreach (var archivo in archivos)
            {
                try
                {
                    nombreArchivo = Path.GetFileName(archivo);
                    pathArchivo = archivo;

                    string pathSalidaArchivo = Path.Combine(pathSalida, nombreArchivo);

                    if (!File.Exists(pathSalidaArchivo))
                    {
                        File.Copy(pathArchivo, pathSalidaArchivo);
                    }
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pathEntrada"></param>
        /// <param name="pathSalida"></param>
        /// <param name="patron"></param>
        /// <returns></returns>
        public List<string> ExtraerTodosArchivosDirectorioXMLListAsync(
            string pathEntrada,
            string pathSalida
        )
        {
            string[] extensions = { "*.xml", "*.XML" }; // Specify the extensions you want to filter

            var archivos_ = Directory.EnumerateFiles(
                pathEntrada,
                "*.xml*",
                SearchOption.AllDirectories
            );

            var archivos = Directory
                .EnumerateFiles(pathEntrada, "*.*", SearchOption.AllDirectories)
                .Where(file => extensions.Contains(Path.GetExtension(file).ToLower()));
            string nombreArchivo = "";
            string pathArchivo = "";
            List<string> listaXMLs = new List<string>();
            archivos = archivos_;

            foreach (var archivo in archivos)
            {
                try
                {
                    nombreArchivo = Path.GetFileName(archivo);
                    pathArchivo = archivo;

                    listaXMLs.Add(pathArchivo);
                }
                catch (Exception) { }
            }

            return listaXMLs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pathEntrada"></param>
        /// <param name="pathSalida"></param>
        /// <param name="patron"></param>
        /// <returns></returns>
        public List<string> ExtraerTodosArchivosDirectorioXMLList(
            string pathEntrada,
            string pathSalida
        )
        {
            string[] extensions = { "*.xml", "*.XML" }; // Specify the extensions you want to filter

            var archivos = Directory.EnumerateFiles(
                pathEntrada,
                "*.xml*",
                SearchOption.AllDirectories
            );
            string nombreArchivo = "";
            string pathArchivo = "";
            List<string> listaXMLs = new List<string>();

            foreach (var archivo in archivos)
            {
                try
                {
                    nombreArchivo = Path.GetFileName(archivo);
                    pathArchivo = archivo;

                    listaXMLs.Add(pathArchivo);
                }
                catch (Exception) { }
            }

            return listaXMLs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paqueteFromBase64String"></param>
        /// <param name="nombrePDF"></param>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DescargarPDFBase64Async(
            string paqueteFromBase64String,
            string nombrePDF,
            string path,
            string token = null
        )
        {
            throw new NotImplementedException();
        }
    }
}
