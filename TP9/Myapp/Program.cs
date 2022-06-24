using System.Text.Json;
public class Program{
    public static void Main(string[] args){
        Console.WriteLine("Ingrese un path:");
        string? path = Console.ReadLine();

        if (Directory.Exists(path)){
            string nombreArchivoJson = path + "/index.json"; //donde se guardan los archivos

            FileStream FS;

            if (!File.Exists(nombreArchivoJson)) //Si no existe el archivo lo crea
            {
                FS = File.Create(nombreArchivoJson);
                FS.Close();
            }

            FS = new FileStream(nombreArchivoJson,FileMode.Open);
            StreamWriter SW = new StreamWriter(FS);
            List<string> ListadoDeCarpetas = Directory.GetDirectories(path).ToList();
            List<Archivos> ListadoDeArchivos = new List<Archivos>();
            int ID=1;
            foreach (string CarpetaPath in ListadoDeCarpetas)
            {
                Console.WriteLine(CarpetaPath);

                //SW.Write(ID + ";" + new DirectoryInfo(CarpetaPath).Name + "; "); //Guardo carpeta

                foreach (string item in Directory.GetFiles(CarpetaPath))
                {
                    Console.WriteLine(item);
                    Archivos archivo = new Archivos();
                    //SW.WriteLine(ID + ";" + Path.GetFileNameWithoutExtension(item) + "; " + Path.GetExtension(item));//Guardo archivo de cada carpeta, primero el nombre y luego la extension
                    archivo.ID=ID;
                    archivo.nombre = Path.GetFileNameWithoutExtension(item);
                    archivo.extension = Path.GetExtension(item);
                    ListadoDeArchivos.Add(archivo);
                    ID++;
                }
            }
            foreach (string item in Directory.GetFiles(path))
            {
                Console.WriteLine(item);
                //SW.WriteLine(ID + ";" + Path.GetFileNameWithoutExtension(item) + "; " + Path.GetExtension(item)); //paso por los archivos de la raiz
                Archivos archivo = new Archivos();
                archivo.ID=ID;
                archivo.nombre = Path.GetFileNameWithoutExtension(item);
                archivo.extension = Path.GetExtension(item);
                ListadoDeArchivos.Add(archivo);
                ID++;
            }

            Console.WriteLine("--Serializando--");
            string archivosJson = JsonSerializer.Serialize(ListadoDeArchivos);
            Console.WriteLine("Archivo Serializado : " + archivosJson);
            Console.WriteLine("--Guardando--");
            SW.WriteLine(archivosJson);

            SW.Close();
            FS.Close();
        }
    }
}
