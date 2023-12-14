using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IDocumentRepository
	{
		public List<Document> GetAllDocuments();
		public void DeleteDocument(long documentId);
		public Document GetDocument(long documentId);
		public void EditDocument(Document document);
		public void AddDocument(Document document);
	}
}
