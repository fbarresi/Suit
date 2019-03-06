﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using log4net;
using ReactiveUI;
using Suit.Interfaces.Commons;
using Suit.Interfaces.Logging;

namespace Suit.Gui.ViewModels
{
	public abstract class ViewModelBase : ReactiveObject, IDisposable, IInitializable
	{
		protected CompositeDisposable Disposables = new CompositeDisposable();
		private bool disposed;
		private string title;
		protected ILog Logger { get; private set; } = LoggerFactory.GetLogger();
		public virtual void Dispose()
		{
			Dispose(true);
		}

		public abstract void Init();

		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "Disposables")]
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			Disposables?.Dispose();
			Disposables = null;

			disposed = true;
		}

		[NotifyPropertyChangedInvocator]
		// ReSharper disable once InconsistentNaming
		protected void raisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			this.RaisePropertyChanged(propertyName);
		}

		public string Title
		{
			get => title;
			set
			{
				if (value == title) return;
				title = value;
				raisePropertyChanged();
			}
		}

		~ViewModelBase()
		{
			Dispose(false);
		}
	}
}