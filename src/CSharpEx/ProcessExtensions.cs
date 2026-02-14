#nullable enable
using System;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#else
using CSharpEx.Versioning;
#endif

namespace CSharpEx
{
	public static partial class ProcessExtensions
	{
#if WINDOWS || NETFRAMEWORK || NET5_0_OR_GREATER
		/// <summary>
		/// Inicia el proceso. En Windows, lo vincula a un Job Object para limpieza automática.
		/// </summary>
		[SupportedOSPlatform("windows5.1.2600")]
		public static bool StartAsJob(this Process process)
		{
			if (!process.Start())
				return false;

			ApplyJobObjectInternal(process);

			return true;
		}
#endif

#if WINDOWS || NETFRAMEWORK || NET5_0_OR_GREATER
		/// <summary>
		/// Crea e inicia un proceso vinculado a un Job Object para asegurar su cierre automático.
		/// </summary>
		/// <param name="fileName">Ruta del ejecutable.</param>
		/// <param name="arguments">Argumentos de línea de comandos.</param>
		[SupportedOSPlatform("windows5.1.2600")]
		public static Process? StartAsJob(string fileName, string arguments)
		{
			Process process = new Process();
			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = arguments;

			if (!process.Start())
				return null;

			ApplyJobObjectInternal(process);

			return process;
		}
#endif

		/// <summary>
		/// Inicia el proceso. En Windows, limita el Working Set (RAM Física).
		/// </summary>
		[SupportedOSPlatform("windows")]
		[SupportedOSPlatform("freebsd")]
		[SupportedOSPlatform("maccatalyst")]
		[SupportedOSPlatform("macOS")]
		[SupportedOSPlatform("OSX")]
		public static bool StartWithRamLimited(this Process process, int ramLimitInMb)
		{
			if (!process.Start())
				return false;

			try
			{
				long bytes = (long)ramLimitInMb * 1024 * 1024;
				process.MaxWorkingSet = checked((IntPtr)bytes);
				return true;
			}
			catch
			{
				return true;
			}
		}

		/// <summary>
		/// Crea e inicia un proceso con un límite de RAM física (Working Set).
		/// </summary>
		/// <param name="fileName">Ruta del ejecutable (ej. java.exe)</param>
		/// <param name="arguments">Argumentos (ej. -jar server.jar)</param>
		/// <param name="ramLimitInMb">Límite de RAM en Megabytes</param>
		[SupportedOSPlatform("windows")]
		[SupportedOSPlatform("freebsd")]
		[SupportedOSPlatform("macOS")]
		public static Process? StartWithRamLimited(string fileName, string arguments, int ramLimitInMb)
		{
			Process process = new Process();
			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = arguments;

			if (!process.Start())
				return null;

			try
			{
				long bytes = (long)ramLimitInMb * 1024 * 1024;
				process.MaxWorkingSet = checked((IntPtr)bytes);
			}
			catch
			{
			}

			return process;
		}

#if WINDOWS || NETFRAMEWORK || NET5_0_OR_GREATER
		[SupportedOSPlatform("windows5.1.2600")]
		private static void ApplyJobObjectInternal(Process process)
		{
			try
			{
				unsafe
				{
					var hJob = Windows.Win32.PInvoke.CreateJobObject(null, (string?)null);

					if (hJob.IsInvalid)
						return;

					Windows.Win32.System.JobObjects.JOBOBJECT_EXTENDED_LIMIT_INFORMATION info = new();
					info.BasicLimitInformation.LimitFlags = Windows.Win32.System.JobObjects.JOB_OBJECT_LIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;

					Windows.Win32.PInvoke.SetInformationJobObject(
						new Windows.Win32.Foundation.HANDLE(hJob.DangerousGetHandle()),
						Windows.Win32.System.JobObjects.JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation,
						&info,
						(uint)sizeof(Windows.Win32.System.JobObjects.JOBOBJECT_EXTENDED_LIMIT_INFORMATION)
					);

					Windows.Win32.PInvoke.AssignProcessToJobObject(
						new Windows.Win32.Foundation.HANDLE(hJob.DangerousGetHandle()),
						new Windows.Win32.Foundation.HANDLE(process.Handle)
					);
				}
			}
			catch { }
		}
#endif
	}
}