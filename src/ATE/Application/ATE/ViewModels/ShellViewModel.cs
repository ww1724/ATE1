using ATE.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATE.ViewModels
{
    public class ShellViewModel : ReactiveObject, IScreen
    {
        public ShellViewModel()
        {
            Locator.CurrentMutable.Register(() => new TestingView(), typeof(IViewFor), "Core.TestingView");

            Router = new RoutingState();
            GoNext = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new TestingViewModel(this)));
            var canGoBack = this.WhenAnyValue(x => x.Router.NavigationStack.Count) .Select(count => count > 0);
            GoBack = ReactiveCommand.CreateFromObservable( () => Router.NavigateBack.Execute(Unit.Default), canGoBack);

            Router.Navigate.Execute(Locator.Current.GetService(typeof(IViewFor), "Core.TestingView"));
        }



        public RoutingState Router { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }


    }
}
