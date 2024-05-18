using System;
using System.IO;

public class Contacto
{
    private string nombre;
    private string numero;
    private string correo;

    public Contacto(string nombre, string numero, string correo)
    {
        this.nombre = nombre;
        this.numero = numero;
        this.correo = correo;
    }

    public void SetNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string GetNombre()
    {
        return this.nombre;
    }

    public void SetNumero(string numero)
    {
        this.numero = numero;
    }

    public string GetNumero()
    {
        return this.numero;
    }

    public void SetCorreo(string correo)
    {
        this.correo = correo;
    }

    public string GetCorreo()
    {
        return this.correo;
    }

    public override string ToString()
    {
        return $@"
        <====================>

        Nombre: {this.nombre}
        Numero: {this.numero}
        Correo: {this.correo}
        
        <====================>
        ";
    }
}

public class Agenda
{
    private Contacto[] miscontactos = new Contacto[50];
    private string archivo = "agenda.txt";

    public Agenda()
    {
        for (int i = 0; i < 50; i++)
        {
            miscontactos[i] = new Contacto("", "", "");
        }

        if (File.Exists(archivo))
        {
            string[] lineas = File.ReadAllLines(archivo);
            for (int i = 0; i < lineas.Length && i < 50; i++)
            {
                string[] datos = lineas[i].Split(',');
                miscontactos[i] = new Contacto(datos[0], datos[1], datos[2]);
            }
        }
        else
        {
            File.Create(archivo).Close();
        }
    }

    public void GuardarCambios()
    {
        using (StreamWriter sw = new StreamWriter(archivo))
        {
            foreach (var contacto in miscontactos)
            {
                if (!string.IsNullOrEmpty(contacto.GetNombre()) && contacto.GetNombre() != "0")
                {
                    sw.WriteLine($"{contacto.GetNombre()},{contacto.GetNumero()},{contacto.GetCorreo()}");
                }
            }
        }
    }

    public void NuevoContacto(string nombre, string numero, string correo)
    {
        for (int i = 0; i < 50; i++)
        {
            if (string.IsNullOrEmpty(miscontactos[i].GetNombre()) || miscontactos[i].GetNombre() == "0")
            {
                miscontactos[i] = new Contacto(nombre, numero, correo);
                break;
            }
        }
    }

    public Contacto BuscarContacto(string criterio)
    {
        foreach (var contacto in miscontactos)
        {
            if (contacto.GetNombre().Equals(criterio, StringComparison.OrdinalIgnoreCase) || contacto.GetNumero().Equals(criterio))
            {
                return contacto;
            }
        }
        return null;
    }

    public void EditarContacto(string criterio, string nuevoNombre, string nuevoNumero, string nuevoCorreo)
    {
        var contacto = BuscarContacto(criterio);
        if (contacto != null)
        {
            contacto.SetNombre(nuevoNombre);
            contacto.SetNumero(nuevoNumero);
            contacto.SetCorreo(nuevoCorreo);
        }
    }

    public void MostrarContacto(string criterio)
    {
        var contacto = BuscarContacto(criterio);
        if (contacto != null)
        {
            Console.WriteLine(contacto.ToString());
        }
        else
        {
            Console.WriteLine("No se encontro el registro");
        }
    }

    public void EliminarContacto(string criterio)
    {
        var contacto = BuscarContacto(criterio);
        if (contacto != null)
        {
            Console.WriteLine("Desea eliminar el registro (S/N)");
            char confirmacion = Console.ReadKey().KeyChar;
            if (char.ToUpper(confirmacion) == 'S')
            {
                contacto.SetNombre("0");
                contacto.SetNumero("0");
                contacto.SetCorreo("0");
            }
        }
        else
        {
            Console.WriteLine("No se puede eliminar el registro");
        }
    }

    public void MostrarTodos()
    {
        foreach (var contacto in miscontactos)
        {
            if (!contacto.GetNombre().Equals("0") && !string.IsNullOrEmpty(contacto.GetNombre()))
            {
                Console.WriteLine(contacto.ToString());
            }
        }
    }

    public void EliminarTodos()
    {
        for (int i = 0; i < 50; i++)
        {
            miscontactos[i] = new Contacto("", "", "");
        }
        File.WriteAllText(archivo, string.Empty);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Agenda agenda = new Agenda();
        ConsoleKeyInfo opcion;
        do
        {
            Console.Clear();
            Console.WriteLine(@"Menu:
            F1) Nuevo
            F2) Buscar
            F3) Editar
            F4) Mostrar
            F5) Eliminar
            F6) Mostrar todos
            F7) Eliminar todos
            F8) Salir");
            opcion = Console.ReadKey();

            switch (opcion.Key)
            {
                case ConsoleKey.F1:
                    Console.WriteLine("\nIngrese el nombre:");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese el numero:");
                    string numero = Console.ReadLine();
                    Console.WriteLine("Ingrese el correo:");
                    string correo = Console.ReadLine();
                    agenda.NuevoContacto(nombre, numero, correo);
                    break;

                case ConsoleKey.F2:
                    Console.WriteLine("\nIngrese el nombre o numero a buscar:");
                    string criterioBuscar = Console.ReadLine();
                    agenda.MostrarContacto(criterioBuscar);
                    Console.ReadKey();
                    break;

                case ConsoleKey.F3:
                    Console.WriteLine("\nIngrese el nombre o numero a editar:");
                    string criterioEditar = Console.ReadLine();
                    var contacto = agenda.BuscarContacto(criterioEditar);
                    if (contacto != null)
                    {
                        Console.WriteLine("Ingrese el nuevo nombre:");
                        string nuevoNombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo numero:");
                        string nuevoNumero = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo correo:");
                        string nuevoCorreo = Console.ReadLine();
                        agenda.EditarContacto(criterioEditar, nuevoNombre, nuevoNumero, nuevoCorreo);
                    }
                    else
                    {
                        Console.WriteLine("Registro invalido");
                    }
                    Console.ReadKey();
                    break;

                case ConsoleKey.F4:
                    Console.WriteLine("\nIngrese el nombre o numero a mostrar:");
                    string criterioMostrar = Console.ReadLine();
                    agenda.MostrarContacto(criterioMostrar);
                    Console.ReadKey();
                    break;

                case ConsoleKey.F5:
                    Console.WriteLine("\nIngrese el nombre o numero a eliminar:");
                    string criterioEliminar = Console.ReadLine();
                    agenda.EliminarContacto(criterioEliminar);
                    Console.ReadKey();
                    break;

                case ConsoleKey.F6:
                    Console.WriteLine("\nMostrando todos los contactos:");
                    agenda.MostrarTodos();
                    Console.ReadKey();
                    break;

                case ConsoleKey.F7:
                    Console.WriteLine("\nEliminando todos los contactos...");
                    agenda.EliminarTodos();
                    Console.ReadKey();
                    break;

                case ConsoleKey.F8:
                    Console.WriteLine("\nGuardando cambios y saliendo...");
                    agenda.GuardarCambios();
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("\nOpcion no valida");
                    Console.ReadKey();
                    break;
            }
            Console.Clear();
        } while (opcion.Key != ConsoleKey.F8);
    }
}
