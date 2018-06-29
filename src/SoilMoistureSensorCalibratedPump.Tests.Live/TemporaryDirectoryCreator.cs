﻿using System;
using System.IO;

namespace SoilMoistureSensorCalibratedPump.Tests.Live
{
	public class TemporaryDirectoryCreator
	{
		public TemporaryDirectoryCreator ()
		{
		}

		public string Create ()
		{
            var projectDir = GetProjectDirectory ();

			var tmpDir = projectDir + "-tmp";

			if (!Directory.Exists (tmpDir))
				Directory.CreateDirectory (tmpDir);

            var guid = Guid.NewGuid ().ToString();
            var key = guid.Substring (0, guid.IndexOf ("-"));

			var uniqueTmpDir = Path.Combine (tmpDir, key);

			Directory.CreateDirectory (uniqueTmpDir);

			return uniqueTmpDir;
		}

        public string GetProjectDirectory()
        {
            // TODO: Move this function to a dedicated component

            var projectDir = Environment.CurrentDirectory;

            var debugString = "/bin/Debug";
            var releaseString = "/bin/Release";

            if (projectDir.EndsWith (debugString))
                projectDir = projectDir.Replace (debugString, "");
            if (projectDir.EndsWith (releaseString))
                projectDir = projectDir.Replace (releaseString, "");

            return projectDir;
        }

	}
}

