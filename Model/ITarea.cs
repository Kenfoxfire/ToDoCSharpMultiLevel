using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Model
{
    internal interface ITarea
    {
        public abstract void editarTarea(string? titulo, string? descripcion, PrioridadEnum? prioridad, DateTime? fechaVencimiento);
        public abstract void MostrarInformacion();
        public abstract void MostrarTitulo();
        public abstract void MarcarTareaCompletada();


        public void AgregarSubtarea(TareaBase subtarea);


        public void AgregarDependenciaATarea(TareaBase tareaDependiente);
        public void EliminarTareaDependienteOSubTarea(string tipo);
        public void CompletarTareaDependienteOSubTarea(string tipo);

    }
}
