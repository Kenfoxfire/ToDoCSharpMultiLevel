using static ToDoApp.Utils.Utils;

namespace ToDoApp.Model
{
    internal class TareaBasica : TareaBase
    {
        public TareaBasica(string titulo, string descripcion) : base(titulo, descripcion, "basica")
        {
            Subtareas = new List<TareaBase>();
            TareasDependientes = new List<TareaBase>();
        }

        public void editarTarea(string titulo, string descripcion)
        {
            Titulo = titulo ?? Titulo;
            Descripcion = descripcion ?? Descripcion;
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
            Console.WriteLine($"-------------------------------------------\nTitulo [{Titulo}]\nDescripcion [{Descripcion}]\nTipo {Tipo}\n-------------------------------------------\n");
        }

        public override void MostrarTitulo()
        {
            Console.WriteLine($"{Titulo}");
        }

        public override void editarTarea(string? titulo, string? descripcion, PrioridadEnum? prioridad, DateTime? fechaVencimiento) //Metodo no requerido en esta clase
        {
        }
    }
}
