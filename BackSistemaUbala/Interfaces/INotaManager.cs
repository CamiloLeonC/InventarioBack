using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface INotaManager
    {
        IQueryable<NotaModel> GetAll(string userId, string userRol);
        NotaModel GetById(int keyNotaModelId);
        NotaModel Add(NotaModel row);
        NotaModel Delete(int keyNotaModelId);
        NotaModel Update(int keyNotaModelId, NotaModel changes);
        string CargarExcel(string filePath);
        public byte[] ExportarNotas();

    }
}
