using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;

namespace TpHereAll
{
    [Command("tphereall")]
    [CommandAlias("tpall")]
    [CommandAlias("tpallhere")]
    [CommandDescription("Teleport all players to you.")]
    [CommandActor(typeof(UnturnedUser))]
    public class TpHereAllCommand : Command
    {
        public TpHereAllCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {}

        protected async override Task OnExecuteAsync()
        {
            await UniTask.SwitchToMainThread();
            int i = 0;
            foreach (var u in TpHereAll.m_userDirectory.GetOnlineUsers())
            {
                if (u == (UnturnedUser)Context.Actor) continue;
                i++;
                u.Player.Player.teleportToPlayer(((UnturnedUser)Context.Actor).Player.Player);
            }
            await UniTask.SwitchToThreadPool();

            await PrintAsync("Teleported " + i + " players!");
        }
    }
}