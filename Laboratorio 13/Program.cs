using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_13
{
    class Program
    {
        const int MAX_CLIENTES = 100;

        struct Cliente
        {
            public string Nombre;
            public int Opinion;
        }

        static void Main(string[] args)
        {
            Cliente[] clientes = new Cliente[MAX_CLIENTES];
            int totalClientes = 0;

            int opcion;
            do
            {
                MostrarMenu();

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RealizarEncuesta(clientes, ref totalClientes);
                            break;
                        case 2:
                            VerDatosRegistrados(clientes, totalClientes);
                            break;
                        case 3:
                            EliminarDato(clientes, ref totalClientes);
                            break;
                        case 4:
                            OrdenarDatos(clientes, ref totalClientes);
                            break;
                        case 5:
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                            break;
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.ReadLine();
                }
            } while (opcion != 5);
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine("Encuestas de Calidad");
            Console.WriteLine("================================");
            Console.WriteLine("1: Realizar Encuesta");
            Console.WriteLine("2: Ver datos registrados");
            Console.WriteLine("3: Eliminar un dato");
            Console.WriteLine("4: Ordenar datos de menor a mayor");
            Console.WriteLine("5: Salir");
            Console.WriteLine("================================");
            Console.Write("Ingrese una opción: ");
        }

        static void RealizarEncuesta(Cliente[] clientes, ref int totalClientes)
        {
            if (totalClientes < MAX_CLIENTES)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("Nivel de Satisfacción");
                Console.WriteLine("===================================");
                Console.WriteLine("¿Qué tan satisfecho está con la\natención de nuestra tienda?");
                Console.WriteLine("1: Nada satisfecho");
                Console.WriteLine("2: No muy satisfecho");
                Console.WriteLine("3: Tolerable");
                Console.WriteLine("4: Satisfecho");
                Console.WriteLine("5: Muy satisfecho");
                Console.WriteLine("===================================");
                Console.Write("Ingrese una opción: ");

                if (int.TryParse(Console.ReadLine(), out int respuesta) && respuesta >= 1 && respuesta <= 5)
                {
                    clientes[totalClientes++] = new Cliente { Opinion = respuesta };

                    Console.Clear();
                    Console.WriteLine("===================================");
                    Console.WriteLine("Nivel de Satisfacción");
                    Console.WriteLine("===================================");
                    Console.WriteLine("\n\n¡Gracias por participar!\n\n");
                    Console.WriteLine("===================================");
                    Console.Write("Presione una tecla para\nregresar al menú ...");
                }
                else
                {
                    Console.WriteLine("Respuesta no válida. Inténtelo de nuevo.");
                }
            }
            else
            {
                Console.WriteLine("Se alcanzó el límite máximo de clientes.");
            }
        }

        static void VerDatosRegistrados(Cliente[] clientes, int totalClientes)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Ver datos registrados");
            Console.WriteLine("===================================");

            if (totalClientes > 0)
            {
                MostrarDatosConIndice(clientes, totalClientes);

                Console.WriteLine();
                MostrarResumenOpiniones(clientes, totalClientes);
            }
            else
            {
                Console.WriteLine("No hay datos registrados.");
            }

            Console.WriteLine("\n===================================");
            Console.Write("Presione una tecla para regresar ...");
        }

        static void MostrarResumenOpiniones(Cliente[] clientes, int totalClientes)
        {
            if (totalClientes > 0)
            {
                int[] conteoOpiniones = new int[5];

                foreach (var cliente in clientes.Take(totalClientes))
                {
                    conteoOpiniones[cliente.Opinion - 1]++;
                }

                Console.WriteLine();
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{conteoOpiniones[i]:D2} personas: {TraducirOpinion(i + 1)}");
                }
            }
            else
            {
                Console.WriteLine("No hay datos registrados.");
            }
        }

        static void EliminarDato(Cliente[] clientes, ref int totalClientes)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Eliminar un dato");
            Console.WriteLine("===================================");

            if (totalClientes > 0)
            {
                MostrarDatosConIndice(clientes, totalClientes);

                Console.WriteLine("\n===================================");
                Console.Write("Ingrese la posición a eliminar: ");

                if (int.TryParse(Console.ReadLine(), out int posicionEliminar) && posicionEliminar >= 1 && posicionEliminar <= totalClientes)
                {
                    for (int i = posicionEliminar - 1; i < totalClientes - 1; i++)
                    {
                        clientes[i] = clientes[i + 1];
                    }

                    totalClientes--;
                    Console.WriteLine("===================================");
                    Console.WriteLine("       Eliminación exitosa");
                    Console.WriteLine("===================================");

                    MostrarDatosConIndice(clientes, totalClientes);
                }
                else
                {
                    Console.WriteLine("Posición no válida. Inténtelo de nuevo.");
                }
            }
            else
            {
                Console.WriteLine("No hay datos registrados para eliminar.");
            }

            Console.WriteLine("===================================");
            Console.Write("Presione una tecla para regresar ...");
            Console.ReadLine();
        }

        static void OrdenarDatos(Cliente[] clientes, ref int totalClientes)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Ordenar datos");
            Console.WriteLine("===================================");

            if (totalClientes > 0)
            {
                MostrarDatosConIndice(clientes, totalClientes);

                Console.WriteLine("\n===================================");
                Console.WriteLine("Datos Ordenados");
                Console.WriteLine("===================================");

                var clientesOrdenados = clientes.Take(totalClientes).OrderBy(cliente => cliente.Opinion).ToArray();

                MostrarDatosConIndice(clientesOrdenados, totalClientes);
            }
            else
            {
                Console.WriteLine("No hay datos registrados para ordenar.");
            }

            Console.WriteLine("\n===================================");
            Console.Write("Presione una tecla para regresar ...");
        }
        static void MostrarDatosConIndice(Cliente[] clientes, int totalClientes)
        {
            for (int i = 0; i < totalClientes; i++)
            {
                Console.Write($"{i:D3}:[{clientes[i].Opinion}]  ");
                if ((i + 1) % 5 == 0 || i == totalClientes - 1)
                {
                    Console.WriteLine();
                }
            }
        }
        static string TraducirOpinion(int numeroOpinion)
        {
            switch (numeroOpinion)
            {
                case 1:
                    return "Nada satisfecho";
                case 2:
                    return "No muy satisfecho";
                case 3:
                    return "Tolerable";
                case 4:
                    return "Satisfecho";
                case 5:
                    return "Muy satisfecho";
                default:
                    return "Desconocido";
            }
        }
    }
}
