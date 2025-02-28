

using static ToDoApp.Utils.Utils;
using System.Threading;

namespace ToDoApp.Model

{
    public abstract class TareaBase : ITarea
    {
        public enum EstadoEnum
        {
            Pendiente,
            Completado,
        }

        protected TareaBase(string titulo, string descripcion, string tipo)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Tipo = tipo;
            Estado = EstadoEnum.Pendiente;
        }

        protected string Titulo { get; set; }
        protected string Descripcion { get; set; }
        public string Tipo { get; set; }
        public EstadoEnum Estado { get; set; }
        public abstract void editarTarea(string? titulo, string? descripcion, PrioridadEnum? prioridad, DateTime? fechaVencimiento);
        public abstract void MostrarInformacion();
        public abstract void MostrarTitulo();
        public abstract void MarcarTareaCompletada();
        public List<TareaBase> Subtareas { get; set; }
        public List<TareaBase> TareasDependientes { get; set; }

        public void AgregarSubtarea(TareaBase subtarea)
        {
            Subtareas.Add(subtarea);
        }

        public void AgregarDependenciaATarea(TareaBase tareaDependiente)
        {
            TareasDependientes.Add(tareaDependiente);
        }

        public void EliminarTareaDependienteOSubTarea(string tipo)
        {

            Console.WriteLine("Ingresa el numero de tarea que desea editar\n");
            if (tipo == "d")
            {
                for (int i = 0; i < TareasDependientes.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    TareasDependientes[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
            }
            if (tipo == "s") {
                for (int i = 0; i < Subtareas.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    Subtareas[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
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

            if (tipo == "d")
            {
                if (numero >= TareasDependientes.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }
                TareasDependientes.RemoveAt(numero);
            }
            if (tipo == "s")
            {
                if (numero >= Subtareas.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }
                Subtareas.RemoveAt(numero);
            }



            Log("Tarea eliminada correctamente", LogType.Info);
        }

        public void CompletarTareaDependienteOSubTarea(string tipo)
        {

            Console.WriteLine("Ingresa el numero de tarea que desea completar\n");
            if (tipo == "d")
            {
                for (int i = 0; i < TareasDependientes.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    TareasDependientes[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
            }
            if (tipo == "s")
            {
                for (int i = 0; i < Subtareas.LongCount(); i++)
                {
                    Console.WriteLine($"[{i}] - ");
                    Subtareas[i].MostrarTitulo();
                    Console.WriteLine("\n");
                }
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

            if (tipo == "d")
            {
                if (numero >= TareasDependientes.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }
                TareasDependientes[numero].MarcarTareaCompletada();
            }
            if (tipo == "s")
            {
                if (numero >= Subtareas.LongCount() || numero < 0)
                {
                    Log("El numero está fuera del rango de tareas", LogType.Warning);
                    return;
                }
                Subtareas[numero].MarcarTareaCompletada();
            }



            Log("Tarea completada correctamente", LogType.Info);
        }
    }
}
