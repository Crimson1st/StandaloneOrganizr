﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StandaloneOrganizr
{
	public class ProgramList
	{
		public List<ProgramLink> programs = new List<ProgramLink>();

		public ProgramList()
		{

		}

		public ProgramList(string src)
		{
			Load(src);
		}

		public void Load(string data)
		{
			string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			if (lines[lines.Length - 1].Trim() == "")
				lines = lines.Reverse().Skip(1).Reverse().ToArray();

			if (lines.Length % 2 != 0)
				throw new IOException();

			for (int i = 0; i < lines.Length; i += 2)
			{
				programs.Add(new ProgramLink(lines[i] + Environment.NewLine + lines[i + 1]));
			}
		}

		public string Save()
		{
			return string.Join(Environment.NewLine, programs.Select(p => p.Save()));
		}

		public List<ProgramLink> find(string search)
		{
			return programs
				.Where(p => p.Find(search) > 0)
				.OrderByDescending(p => p.Find(search))
				.ToList();
		}

		public bool ContainsFolder(string path)
		{
			return programs.Any(p => p.directory.ToLower() == Path.GetFileName(path).ToLower());
		}
	}
}