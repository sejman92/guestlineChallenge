﻿namespace GuestlineChallenge.Commands.Handlers;

public interface ICommandHandler<in T> where T : ICommand
{
    void Handle(T command);
}