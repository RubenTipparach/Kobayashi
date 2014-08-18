using System;

namespace FleetHackers
{
#if WINDOWS || XBOX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			// TO DO: Create state machine.
			using (MainGame game = new MainGame())
			{
				game.Run();
			}
		}
	}
#endif
}

