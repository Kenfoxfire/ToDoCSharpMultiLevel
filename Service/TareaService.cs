
using static ToDoApp.Utils.Utils;
using ToDoApp.Model;

namespace ToDoApp.Service
{
    public class TareaService : ITareaService
    {
        private List<TareaBase> tareas = new List<TareaBase>();

        public void CreateTareaMain()
        {
            Console.WriteLine("Su tarea es principal? s/n");
            var eleccion = Console.ReadLine().ToLower();
            if (new[] { "s", "si", "n", "no" }.Contains(eleccion.ToLower()))
            {
                if (eleccion == "n" || eleccion == "no")
                {
                    Log("Creando SubTarea", LogType.Info);
                    try
                    {
                        if (this.tareas.Count == 0)
                        {
                            Log("No hay tareas principal para agregarle una subtarea", LogType.Info);
                            return;
                        }
                        Console.WriteLine("Ingresa el numero de tarea principal que desea agregarle una subtarea\n");
                        for (int i = 0; i < tareas.LongCount(); i++)
                        {
                            Console.WriteLine($"[{i}] - ");
                            tareas[i].MostrarTitulo();
                            Console.WriteLine("\n");
                        }
                        int numero = -1;
                        try
                        {
                            numero = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Log("No ingresó un numero valido", LogType.Error);
                            return;
                        }
                        catch (OverflowException)
                        {
                            Log("El numero no cumple con las condiciones", LogType.Error);
                            return;
                        }
                        catch (Exception err)
                        {
                            Log($"Ocurrió un error : {err.Message}", LogType.Error);
                            return;
                        }

                        if (numero >= tareas.LongCount() || numero < 0)
                        {
                            Log("El numero está fuera del rango de tareas", LogType.Warning);
                            return;
                        }

                        var nuevaSubTarea = CreateTarea();
                        if (nuevaSubTarea == null) return;
                        tareas[numero].AgregarSubtarea(nuevaSubTarea);
                        Log("SubTarea agreagada correctamente", LogType.Info);

                        Console.WriteLine("Su tarea es dependiente? s/n");
                        var eleccion2 = Console.ReadLine().ToLower();

                        if (new[] { "s", "si", "n", "no" }.Contains(eleccion2.ToLower()))
                        {
                            if (new[] { "s", "si" }.Contains(eleccion2.ToLower()))
                            {
                                tareas[numero].AgregarDependenciaATarea(nuevaSubTarea);

                            }
                        }
                        else
                        {
                            Log("El dato ingresado no es el esperado...\nPor favor intentelo de nuevo", LogType.Warning);
                        }

                    }

                    catch (Exception)
                    {
                        Log("Ha ocurrido un error", LogType.Error);
                    }
                    return;
                }

                Log("Creando Tarea Principal", LogType.Info);
                var nuevaTarea = CreateTarea();
                if (nuevaTarea == null) return;
                tareas.Add(nuevaTarea);
                Log("Tarea agregada con exito!", LogType.Info);
            }
            else
            {
                Log("El dato ingresado no es el esperado...\nPor favor intentelo de nuevo", LogType.Warning);
            }


        }

        private TareaBase? CreateTarea()
        {
            try
            {
                Console.WriteLine("Ingrese el Nombre");
                var nombre = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Log("El dato ingresado esta vacio\nPor favor intentelo de nuevo", LogType.Warning);
                    return null;
                }
                Console.WriteLine("Ingrese la Descripcion");
                var desc = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(desc))
                {
                    Log("El dato ingresado esta vacio\nPor favor intentelo de nuevo", LogType.Warning);
                    return null;
                }
                string vencimiento;
                getResouestaSiNo(out vencimiento, "Desea agregar una fecha de vencimiento? S/N");
                if (vencimiento == "error") return null;

                string prioridad;
                getResouestaSiNo(out prioridad, "Desea agregar una prioridad? S/N");
                if (prioridad == "error") return null;

                if (new[] { "s", "si", "n", "no" }.Contains(vencimiento.ToLower()))
                {
                    if (vencimiento == "n" && prioridad == "n")
                    {
                        return (new TareaBasica(nombre, desc));
                    }
                    else
                    {
                        if (vencimiento == "s" && prioridad == "s")
                        {

                            Console.WriteLine("Ingrese la prioridad A = alta, M = media, B = baja");
                            var prioridadSeleccion = (Console.ReadLine() ?? "").Trim().ToUpper();
                            PrioridadEnum? prioridadEnum;
                            if (prioridadSeleccion == "")
                            {
                                Log("El dato ingresado esta vacio\nPor favor intentelo de nuevo", LogType.Warning);
                                return null;
                            }

                            switch (prioridadSeleccion)
                            {
                                case "A":
                                    prioridadEnum = PrioridadEnum.alta;
                                    break;
                                case "M":
                                    prioridadEnum = PrioridadEnum.media;
                                    break;
                                case "B":
                                    prioridadEnum = PrioridadEnum.baja;
                                    break;
                                default:
                                    Log("El dato ingresado es incorrecto\nPor favor intentelo de nuevo", LogType.Warning);
                                    return null;
                            }
                            DateTime fechaV;
                            try
                            {
                                Console.WriteLine("Ingrese la fecha de vencimiento formato DD-MM-AAAA");
                                fechaV = DateTime.ParseExact(Console.ReadLine() ?? "", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("El formato de la fecha no es válido\nPor favor intentelo de nuevo");
                                return null;
                            }
                            return (new TareaNormal(nombre, desc, "normal", prioridadEnum, fechaV));

                        }
                        else if (vencimiento == "s")
                        {
                            Console.WriteLine("Ingrese la fecha de vencimiento formato DD-MM-AAAA");
                            DateTime fechaV;
                            try
                            {
                                fechaV = DateTime.ParseExact(Console.ReadLine() ?? "", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("El formato de la fecha no es válido\nPor favor intentelo de nuevo");
                                return null;
                            }
                            this.tareas.Add(new TareaNormal(nombre, desc, "normal", null, fechaV));
                        }
                        else
                        {
                            Console.WriteLine("Ingrese la prioridad A = alta, M = media, B = baja");
                            var prioridadSeleccion = (Console.ReadLine() ?? "").Trim().ToUpper();
                            PrioridadEnum? prioridadEnum;
                            if (prioridadSeleccion == "")
                            {
                                Log("El dato ingresado esta vacio\nPor favor intentelo de nuevo", LogType.Warning);
                                return null;
                            }

                            switch (prioridadSeleccion)
                            {
                                case "A":
                                    prioridadEnum = PrioridadEnum.alta;
                                    break;
                                case "M":
                                    prioridadEnum = PrioridadEnum.media;
                                    break;
                                case "B":
                                    prioridadEnum = PrioridadEnum.baja;
                                    break;
                                default:
                                    Log("El dato ingresado es incorrecto\nPor favor intentelo de nuevo", LogType.Warning);
                                    return null;
                            }
                            return (new TareaNormal(nombre, desc, "normal", prioridadEnum, null));
                        }
                    }
                }
                else
                {
                    Log("El dato ingresado no es el esperado...\nPor favor intentelo de nuevo", LogType.Warning);
                    vencimiento = "error";
                }




            }
            catch (IOException)
            {
                Log("Error al obtener los datos por consola", LogType.Error);

            }
            return null;
        }

        public void MostrarTareas(TareaBase.EstadoEnum estado)
        {
            List<TareaBase> tareas = new List<TareaBase>();
            if (estado == TareaBase.EstadoEnum.Pendiente)
            {
                tareas = this.tareas.Where(t => t.Estado == TareaBase.EstadoEnum.Pendiente).ToList();
                if (tareas.Count == 0)
                {
                    Log("No hay tareas para mostrar", LogType.Info);
                    return;
                }
            }
            else
            {
                tareas = this.tareas.Where(t => t.Estado == TareaBase.EstadoEnum.Completado).ToList();
                if (tareas.Count == 0)
                {
                    Log("No hay tareas para mostrar", LogType.Info);
                    return;
                }
            }

            foreach (var tarea in tareas)
            {
                tarea.MostrarInformacion();
            }
        }

        private static void getResouestaSiNo(out string vencimiento, string texto)
        {
            Console.WriteLine(texto);
            vencimiento = Console.ReadLine() ?? "";
            if (new[] { "s", "si", "n", "no" }.Contains(vencimiento.ToLower()))
            {
                if (new[] { "s", "si" }.Contains(vencimiento.ToLower()))
                {
                    vencimiento = "s";
                }
                else
                {
                    vencimiento = "n";
                }
            }
            else
            {
                Log("El dato ingresado no es el esperado...\nPor favor intentelo de nuevo", LogType.Warning);
                vencimiento = "error";
            }
        }

        public void EditarTarea()
        {
            if (this.tareas.Count == 0)
            {
                Log("No hay tareas para mostrar", LogType.Info);
                return;
            }
            Console.WriteLine("Ingresa el numero de tarea que desea editar\n");
            for (int i = 0; i < tareas.LongCount(); i++)
            {
                Console.WriteLine($"[{i}] - ");
                tareas[i].MostrarTitulo();
                Console.WriteLine("\n");
            }
            int numero = -1;
            try
            {
                numero = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Log("No ingresó un numero valido", LogType.Error);
                return;
            }
            catch (OverflowException)
            {
                Log("El numero no cumple con las condiciones", LogType.Error);
                return;
            }
            catch (Exception err)
            {
                Log($"Ocurrió un error : {err.Message}", LogType.Error);
                return;
            }

            if (numero >= tareas.LongCount() || numero < 0)
            {
                Log("El numero está fuera del rango de tareas", LogType.Warning);
                return;
            }

            var tareaSeleccionada = tareas[numero];
            Log("$\"Editando la tarea: {tareaSeleccionada.Tipo}\"", LogType.Info);

            Console.Write("Nuevo titulo (dejar vacío para no cambiar dato) ");
            string nuevoTitulo = Console.ReadLine();

            Console.Write("Nueva descripción (dejar vacío para no cambiar dato) ");
            string nuevaDescripcion = Console.ReadLine();

            if (tareaSeleccionada is TareaNormal tareaNormal)
            {
                Console.WriteLine("Ingrese la prioridad A = alta, M = media, B = baja");
                var prioridadSeleccion = (Console.ReadLine() ?? "").Trim().ToUpper();
                PrioridadEnum? prioridadEnum = null;
                if (prioridadSeleccion != "")
                {
                    switch (prioridadSeleccion)
                    {
                        case "A":
                            prioridadEnum = PrioridadEnum.alta;
                            break;
                        case "M":
                            prioridadEnum = PrioridadEnum.media;
                            break;
                        case "B":
                            prioridadEnum = PrioridadEnum.baja;
                            break;
                        default:
                            Log("El dato ingresado es incorrecto\nPor favor intentelo de nuevo", LogType.Warning);
                            return;
                    }
                }

                Console.WriteLine("Ingrese la fecha de vencimiento formato DD-MM-AAAA");
                DateTime? fechaV = null;
                var fechaIngresada = Console.ReadLine() ?? "";
                if (fechaIngresada != "")
                {
                    try
                    {
                        fechaV = DateTime.ParseExact(fechaIngresada, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Console.WriteLine(fechaV);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("El formato de la fecha no es válido\nPor favor intentelo de nuevo");
                        return;
                    }
                }
                tareaSeleccionada.editarTarea(nuevoTitulo, nuevaDescripcion, prioridadEnum, fechaV);
            }
            else if (tareaSeleccionada is TareaBasica tareaBasica)
            {
                tareaBasica.editarTarea(nuevoTitulo, nuevaDescripcion);
            }

        }

        public void agregarSubTarea()
        {
            try
            {
                if (this.tareas.Count == 0)
                {
                    Log("No hay tareas principales para mostrar", LogType.Info);
                    return;
                }
                Console.WriteLine("Ingresa el numero de tarea principales que le desea agregar\n");
                for (int i = 0; i < tareas.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    tareas[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
                int numero = -1;
                try
                {
                    numero = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Log("No ingresó un numero valido", LogType.Error);
                    return;
                }
                catch (OverflowException)
                {
                    Log("El numero no cumple con las condiciones", LogType.Error);
                    return;
                }
                catch (Exception err)
                {
                    Log($"Ocurrió un error : {err.Message}", LogType.Error);
                    return;
                }

                if (numero >= tareas.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }

                var nuevaTarea = CreateTarea();
                if (nuevaTarea == null) return;
                tareas.Add(nuevaTarea);
                tareas[numero].AgregarSubtarea(nuevaTarea);

                Log("Desea que la tarea sea dependiente\n s/n", LogType.Info);
                var eleccion = Console.ReadLine().ToLower();

                if (new[] { "s", "si", "n", "no" }.Contains(eleccion.ToLower()))
                {

                    if (eleccion == "s" || eleccion == "si")
                    {
                        tareas[numero].AgregarDependenciaATarea(nuevaTarea);
                    }

                    tareas.RemoveAt(numero);
                    Log("Tarea eliminada correctamente", LogType.Info);
                }
                else
                {
                    Log("El dato ingresado no es el esperado...\nPor favor intentelo de nuevo", LogType.Warning);
                    eleccion = "error";
                }
            }
            catch (Exception)
            {
                Log("Ha ocurrido un error", LogType.Error);
            }
        }
        public void EliminarTarea()
        {
            try
            {
                if (this.tareas.Count == 0)
                {
                    Log("No hay tareas para mostrar", LogType.Info);
                    return;
                }
                Console.WriteLine("Ingresa el numero de tarea que desea eliminar\n");
                for (int i = 0; i < tareas.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    tareas[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
                int numero = -1;
                try
                {
                    numero = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Log("No ingresó un numero valido", LogType.Error);
                    return;
                }
                catch (OverflowException)
                {
                    Log("El numero no cumple con las condiciones", LogType.Error);
                    return;
                }
                catch (Exception err)
                {
                    Log($"Ocurrió un error : {err.Message}", LogType.Error);
                    return;
                }

                if (numero >= tareas.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }

                if (tareas[numero].Subtareas.Count > 0)
                {
                    Log("La tarea tiene sub tareas\n¿Desea eliminar una subtarea? s/n", LogType.Info);
                    var eleccion = Console.ReadLine().ToLower();
                    if (eleccion == "s" || eleccion == "si")
                    {
                        tareas[numero].EliminarTareaDependienteOSubTarea("s");
                    }
                    
                }

                if (tareas[numero].TareasDependientes.Count > 0)
                {
                    Log("La tarea tiene tareas dependientes\n¿Desea eliminar una tarea dependiente? s/n", LogType.Info);
                    var eleccion = Console.ReadLine().ToLower();
                    if (eleccion == "s" || eleccion == "si")
                    {
                        tareas[numero].EliminarTareaDependienteOSubTarea("d");
                    }
                    
                }

                Log("Desea eliminar la tarea principal? s/n", LogType.Info);
                var eleccion3 = Console.ReadLine().ToLower();
                if (eleccion3 == "s" || eleccion3 == "si")
                {
                    tareas.RemoveAt(numero);
                    Log("Tarea eliminada correctamente", LogType.Info);
                }

                
            }
            catch (Exception)
            {
                Log("Ha ocurrido un error", LogType.Error);
            }
        }


        public void CompletarTarea()
        {
            try
            {
                if (this.tareas.Count == 0)
                {
                    Log("No hay tareas para mostrar", LogType.Info);
                    return;
                }
                Console.WriteLine("Ingresa el numero de tarea que desea completar\n");
                for (int i = 0; i < tareas.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    tareas[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
                int numero = -1;
                try
                {
                    numero = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Log("No ingresó un numero valido", LogType.Error);
                    return;
                }
                catch (OverflowException)
                {
                    Log("El numero no cumple con las condiciones", LogType.Error);
                    return;
                }
                catch (Exception err)
                {
                    Log($"Ocurrió un error : {err.Message}", LogType.Error);
                    return;
                }

                if (numero >= tareas.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }

                if (tareas[numero].Subtareas.Count > 0)
                {
                    Log("La tarea tiene sub tareas\n¿Desea completar una subtarea? s/n", LogType.Info);
                    var eleccion = Console.ReadLine().ToLower();
                    if (eleccion == "s" || eleccion == "si")
                    {
                        tareas[numero].CompletarTareaDependienteOSubTarea("s");
                    }
                }
                if (tareas[numero].TareasDependientes.Count > 0)
                {
                    Log("La tarea tiene tareas dependientes\n¿Desea completar una tarea dependiente? s/n", LogType.Info);
                    var eleccion = Console.ReadLine().ToLower();
                    if (eleccion == "s" || eleccion == "si")
                    {
                        tareas[numero].CompletarTareaDependienteOSubTarea("d");
                    }
                }

                Log("Desea marcar como completada la tarea principal? s/n", LogType.Info);
                var eleccion3 = Console.ReadLine().ToLower();
                if (eleccion3 == "s" || eleccion3 == "si")
                {
                    tareas[numero].MarcarTareaCompletada();
                    Log("Tarea completada correctamente", LogType.Info);
                }


            }
            catch (Exception)
            {
                Log("Ha ocurrido un error", LogType.Error);
            }
        }
    }
}
