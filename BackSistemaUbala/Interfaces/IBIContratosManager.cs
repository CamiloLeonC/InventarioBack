using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IContratosManager
    {
        IQueryable<ContratosModel> GetAll();
        ContratosModel GetById(int keyContratosModel);
        ContratosModel Add(ContratosModel row);
        ContratosModel Delete(int keyContratosModel);
        ContratosModel Update(int keyContratosModel, ContratosModel changes);
        public byte[] ExportarContratos();
    }
}
