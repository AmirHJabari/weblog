namespace Weblog.WebWasm.Shared;

public partial class MainLayout : LayoutComponentBase
{
    [Inject]
    public ILocalStorageService LocalStorage { get; set; }
    private string _themeIcon;
    bool _drawerOpen = true;
    private string _themeName = "light";
    MudTheme _currentTheme = new()
    {
        Palette = new Palette
        {
            Primary = Colors.Green.Default,
            Secondary = Colors.Amber.Default
        }
    };
    MudTheme _darkTheme = new()
    {
        Palette = new() 
        {
            AppbarBackground = "#0097ff",
            AppbarText = "#ffffff",
            Primary = "#007cd1",
            TextPrimary = "#ffffff",
            Background = "#001524",
            TextSecondary = "#e2eef6",
            DrawerBackground = "#093958",
            DrawerText  = "#ffffff",
            Surface = "#093958",
            ActionDefault = "#0097ff",
            ActionDisabled = "#2f678c",
            TextDisabled = "#b0b0b0",
        },
    };
    MudTheme _lightTheme = new()
    {
        Palette = new() 
        {
            AppbarBackground = "#0097ff",
            AppbarText = "#ffffff",
            Primary = "#007cd1",
            TextPrimary = "#0c1217",
            Background = "#f4fdff",
            TextSecondary = "#0c1217",
            DrawerBackground = "#c5e5ff",
            DrawerText  = "#0c1217",
            Surface = "#c5e5ff",
            ActionDefault = "#0c1217",
            ActionDisabled = "#2f678c",
            TextDisabled = "#676767",
            
        },
    };

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override async Task OnInitializedAsync()
    {
        _themeName = await LocalStorage.GetItemAsStringAsync("theme");
        // UpdateTheme();
        StateHasChanged();
    }

    private async Task ChangeThemeAsync()
    {
        if (_themeName is "light")
        {
            _themeName = "dark";
        }
        else if (_themeName is "dark")
        {
            _themeName = "light";
        }

        await LocalStorage.SetItemAsStringAsync("theme", _themeName);
        // UpdateTheme();
    }

    // private void UpdateTheme()
    // {
    //     if (_themeName is "light")
    //     {
    //         _themeIcon = Icons.Filled.LightMode;
    //         _currentTheme = _darkTheme;
    //     }
    //     else if (_themeName is "dark")
    //     {
    //         _themeIcon = Icons.Outlined.DarkMode;
    //         _currentTheme = _lightTheme;
    //     }
    // }


    private ThemeManagerTheme _themeManager = new()
    {
        DrawerClipMode = DrawerClipMode.Docked,
        DrawerElevation = 3,
        AppBarElevation = 25,
        DefaultElevation = 25,
        DefaultBorderRadius = 5
    };
    bool _themeManagerOpen = false;

    void OpenThemeManager(bool value)
    {
        _themeManagerOpen = value;
    }

    void UpdateTheme(ThemeManagerTheme value)
    {
        if (_themeName is "light")
        {
            _themeIcon = Icons.Filled.LightMode;
            _currentTheme = _darkTheme;
        }
        else if (_themeName is "dark")
        {
            _themeIcon = Icons.Outlined.DarkMode;
            _currentTheme = _lightTheme;
        }
        
        _themeManager = value;
        StateHasChanged();
    }
}