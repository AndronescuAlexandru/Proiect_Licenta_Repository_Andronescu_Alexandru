using System.IO;
using System.Threading.Tasks;

namespace Proiect_Licenta
{ 
	public interface ISave
	{
		//Method to save document as a file and view the saved document
		Task SaveAndView(string filename, string contentType, MemoryStream stream);
		Task<byte[]> CaptureAsync();
	}
	public interface ISaveWindowsPhone
	{
		Task Save(string filename, string contentType, MemoryStream stream);
		Task<byte[]> CaptureAsync();
	}
}

