using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;

namespace ToDoApp.Service
{
    internal interface ITareaService
    {
        public void CompletarTarea();

        public void EliminarTarea();

        public void agregarSubTarea();

        public void EditarTarea();

        public void MostrarTareas(TareaBase.EstadoEnum estado);

        public void CreateTareaMain();

    }
}
