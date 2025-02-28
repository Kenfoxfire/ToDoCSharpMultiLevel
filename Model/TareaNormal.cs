using static ToDoApp.Utils.Utils;

namespace ToDoApp.Model
{
    public enum PrioridadEnum
    {
        alta,
        media,
        baja
    }
    internal class TareaNormal : TareaBase
    {
        public TareaNormal(string titulo, string descripcion, string tipo, PrioridadEnum? prioridad, DateTime? fechaVencimiento) : base(titulo, descripcion, tipo)
        {
            Prioridad = prioridad ?? PrioridadEnum.baja;
            FechaVencimiento = fechaVencimiento ?? DateTime.ParseExact("00-00-0000", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Subtareas = new List<TareaBase>();
            TareasDependientes = new List<TareaBase>();
        }

        private PrioridadEnum Prioridad { get; set; }
        private DateTime FechaVencimiento { get; set; }

        public override void editarTarea(string? titulo, string? descripcion, PrioridadEnum? prioridad, DateTime? fechaVencimiento)
        {
            Titulo = titulo ?? Titulo;
            Descripcion = descripcion ?? Descripcion;
            Prioridad = prioridad ?? Prioridad;
            FechaVencimiento = fechaVencimiento ?? FechaVencimiento;
        }


        public override void MarcarTareaCompletada()
        {
            foreach (var tareaDependiente in TareasDependientes)
            {
                if (tareaDependiente.Estado != EstadoEnum.Completado)
                {
                    Log($"No se puede completar {Titulo} porque la tarea dependiente", LogType.Warning);
                    tareaDependiente.MostrarTitulo();
                    Log($"no esta completa", LogType.Warning);
                    return;
                }
            }

            Estado = EstadoEnum.Completado;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine($"-------------------------------------------\nTitulo [{Titulo}]\nDescripcion [{Descripcion}]\nTipo [{Tipo}]\nPrioridad [{Prioridad}]\nFecha de Vencimiento [{FechaVencimiento}]\n-------------------------------------------\n");
        }

        public override void MostrarTitulo()
        {
            Console.WriteLine($"{Titulo}");
        }
    }
}
