using System.Text.Json;
public class Program{

    private static string[] NombreProductos = {"Pan","Fideos","Arroz","Google Pixel 6"};
    private static string[] TamañosProductos = {"Pequeño","Mediano","Grande"};
    public static void Main(string[] args){
        Console.WriteLine("Ingrese la cantidad de productos:");
        int cantidadProductos=Int32.Parse(Console.ReadLine());
        List<Producto> ListaDeProductos= new List<Producto>();
        for (int i = 1; i < cantidadProductos; i++)
        {
            Producto nuevoProducto = new Producto();
            nuevoProducto = CrearProductoAleatorio();
            ListaDeProductos.Add(nuevoProducto);
        }

        Console.WriteLine("Ingrese un path:");
        string? path = Console.ReadLine();
        var miHelperdeArchivos = new HelperDeJson();
        if (Directory.Exists(path)){
            string nombreArchivoJson = path + "/productos.json"; //donde se guardan los archivos

            Console.WriteLine("--Serializando--");
            string productosJson = JsonSerializer.Serialize(ListaDeProductos);
            Console.WriteLine("Archivo Serializado : " + productosJson);
            Console.WriteLine("--Guardando--");
            miHelperdeArchivos.GuardarArchivoTexto(nombreArchivoJson,productosJson);
        }
    }
    

    public static Producto CrearProductoAleatorio(){
        Random rnd = new Random();
        Producto nuevoProducto = new Producto();
        nuevoProducto.nombre = NombreProductos[rnd.Next(NombreProductos.Length)];
        nuevoProducto.fechavencimiento = new DateTime (rnd.Next(2022,2030),rnd.Next(1,13),rnd.Next(1,29)); //cambiar para que no pida la hora
        nuevoProducto.precio = rnd.Next(1000,5000);
        nuevoProducto.tamanio = TamañosProductos[rnd.Next(TamañosProductos.Length)];
        return nuevoProducto;
    }
}
