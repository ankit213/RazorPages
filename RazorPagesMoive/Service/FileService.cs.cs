﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using RazorPagesMoive.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RazorPagesMoive.Service
{
	public class FileService
	{
		private readonly IMemoryCache _cache;
		private readonly IFileProvider _fileProvider;
		private List<string> _tokens = new List<string>();

		public FileService(IMemoryCache cache, IHostingEnvironment env)
		{
			_cache = cache;
			_fileProvider = env.ContentRootFileProvider;
		}

		public async Task<string> GetFileContents(string fileName)
		{
			// For the purposes of this example, files are stored 
			// in the content root of the app. To obtain the physical
			// path to a file at the content root, use the
			// ContentRootFileProvider on IHostingEnvironment.
			var filePath = _fileProvider.GetFileInfo(fileName).PhysicalPath;
			string fileContent;

			// Try to obtain the file contents from the cache.
			if (_cache.TryGetValue(filePath, out fileContent))
			{
				return fileContent;
			}

			// The cache doesn't have the entry, so obtain the file 
			// contents from the file itself.
			fileContent = await Utilities.GetFileContent(filePath);

			if (fileContent != null)
			{
				// Obtain a change token from the file provider whose
				// callback is triggered when the file is modified.
				var changeToken = _fileProvider.Watch(fileName);

				// Configure the cache entry options for a five minute
				// sliding expiration and use the change token to
				// expire the file in the cache if the file is
				// modified.
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(5))
					.AddExpirationToken(changeToken);

				// Put the file contents into the cache.
				_cache.Set(filePath, fileContent, cacheEntryOptions);

				return fileContent;
			}

			return string.Empty;
		}
	}
}