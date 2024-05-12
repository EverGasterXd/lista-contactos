using System.Collections;
using System.IO;

public class persona
{
    private string nombre;
    public string numero;
    public string correo;

    public persona(string nombre, string numero, string correo)
    {
        this.nombre = nombre;
        this.numero = numero;
        this.correo = correo;
    }

    public void setNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string getNombre()
    {
        return this.nombre;
    }

    public void setNumero(string numero)
    {
        this.numero = numero;
    }

    public string getNumero()
    {
        return this.numero;
    }

    public void setCorreo(string correo)
    {
        this.correo = correo;
    }

    public string getCorreo()
    {
        return this.correo;
    }

    public override bool Equals(object obj)
    {
        if (obj is persona other)
        {
            return this.nombre == other.nombre;
        }
        return false;
    }

    public override string ToString()
    {
        return @$"
        Nombre: {this.nombre}
        Numero: {this.numero}
        Correo: {this.correo}";
    }
}

public class croud
{
    ArrayList personas;
    string path = "contactos.txt";

    public croud()
    {
        personas = new ArrayList();
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                personas.Add(new persona(data[0], data[1], data[2]));
            }
        }
    }

    public void agregar(persona p)
    {
        personas.Add(p);
        File.AppendAllText(path, $"{p.getNombre()},{p.getNumero()},{p.getCorreo()}\n");
    }

    public void mostrar()
    {
        foreach (persona p in personas)
        {
            Console.WriteLine(p.ToString());
        }
    }

    public persona buscar(string nombre)
    {
        foreach (persona p in personas)
        {
            if (p.getNombre() == nombre)
                return p;
        }
        return null;
    }

    public bool eliminar(persona b)
    {
        persona tmp = buscar(b.getNombre());

        if (tmp != null)
        {
            personas.Remove(tmp);
            return true;
        }
        return false;
    }

    public bool actualizar(persona a)
    {
        persona tmp = buscar(a.getNombre());
        if (tmp != null)
        {
            personas.Remove(tmp);
            string nombre;
            string telefono;
            string correo;
            Console.WriteLine("ingrese el nombre");
            nombre = Console.ReadLine();
            Console.WriteLine("ingrese el telefono");
            telefono = Console.ReadLine();
            Console.WriteLine("ingrese el correo");
            correo = Console.ReadLine();
            tmp.setNombre(nombre);
            tmp.setNumero(telefono);
            tmp.setCorreo(correo);
            
            personas.Add(tmp);

            return true;
        }
        return false;
    }

    public void BuscarYImprimir(string nombre)
    {
        persona resultado = buscar(nombre);
    
        if (resultado != null)
        {
            Console.WriteLine("Persona encontrada: " + resultado.ToString());
        }
        else
        {
            Console.WriteLine("Persona no encontrada");
        }
    }
}

public class program
{
    public static void Main(string[] args)
    {
        try
        {
            croud c = new croud();
            int op;
            do
            {
                Console.WriteLine("1.- agregar");
                Console.WriteLine("2.- eliminar");
                Console.WriteLine("3.- actualizar");
                Console.WriteLine("4.- salir");
                Console.WriteLine("5.- mostrar");
                op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        string nombre;
                        string telefono;
                        string correo;
                        Console.WriteLine("ingrese el nombre");
                        nombre = Console.ReadLine();
                        Console.WriteLine("ingrese el telefono");
                        telefono = Console.ReadLine();
                        Console.WriteLine("ingrese el correo");
                        correo = Console.ReadLine();
                        c.agregar(new persona(nombre, telefono, correo));
                        Console.WriteLine("se agrego correctamente");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        string nombre2;
                        Console.WriteLine("ingrese el nombre");
                        nombre2 = Console.ReadLine();
                        if (c.eliminar(new persona(nombre2, "", "")))
                        {
                            Console.WriteLine("se elimino correctamente");
                        }
                        else
                        {
                            Console.WriteLine("no se encontro el nombre");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        string nombre3;
                        Console.WriteLine("ingrese el nombre");
                        nombre3 = Console.ReadLine();
                        if (c.actualizar(new persona(nombre3, "", "")))
                        {
                            Console.WriteLine("se actualizo correctamente");
                        }
                        else
                        {
                            Console.WriteLine("no se encontro el nombre");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine("adios");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.WriteLine("ingresa el nombre de la persona que quieres buscar");
                        string nombre4 = Console.ReadLine();
                        c.BuscarYImprimir(nombre4);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("opcion no valida");
                        Console.Clear();
                        break;
                }
            } while (op != 4);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}