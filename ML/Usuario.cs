using System;
namespace ML
{
	public class Usuario
	{
		public Usuario()
		{
		}
		public int IdUsuario { get; set; }
		public string Nombre { get; set; }
		public string ApellidoPatenro { get; set; }
		public string ApellidoMaterno { get; set; }
		public string Email { get; set; }
		public byte[] Password { get; set; }
	}
}

