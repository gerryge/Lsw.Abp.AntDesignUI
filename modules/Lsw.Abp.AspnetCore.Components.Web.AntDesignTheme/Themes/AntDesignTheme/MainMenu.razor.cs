﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class MainMenu : IDisposable
{
    protected ApplicationMenu Menu { get; set; }
    
    [Inject]
    protected IMenuManager MenuManager { get; set; }

    [Inject]
    protected IOptions<AbpAntDesignThemeOptions> Options { get; set; }
    
    [Inject]
    protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    
    [Parameter]
    public bool Collapsed { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetMenuAsync();
        AuthenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProviderOnAuthenticationStateChanged;
    }

    private async Task GetMenuAsync()
    {
        Menu = await MenuManager.GetMainMenuAsync();
    }
    
    private async void AuthenticationStateProviderOnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        await GetMenuAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        AuthenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProviderOnAuthenticationStateChanged;
    }
}
