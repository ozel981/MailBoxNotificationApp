
using System;
using System.Reflection;
using Microsoft.Win32;
using Newtonsoft.Json;

internal class Win32Registry
{
    private readonly RegistryKey _key;

    public Win32Registry()
    {
        var softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
        var keyName = Assembly.GetExecutingAssembly().GetName().Name;
        if (softwareKey != null)
        {
            _key = softwareKey.OpenSubKey(keyName, true) ?? softwareKey.CreateSubKey(keyName);
        }
    }

    private string Get(string option, string defaultValue = null)
    {
        if (option == null)
        {
            throw new ArgumentNullException(nameof(option));
        }

        if (_key != null)
        {
            try
            {
                if (_key.GetValueKind(option) == RegistryValueKind.String)
                {
                    return (string)_key.GetValue(option);
                }
            }
            catch
            {
                // ignored
            }
        }

        return defaultValue;
    }

    public T Get<T>(string option, T defaultValue = default)
    {
        var result = Get(option);
        if (result != null)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch
            {
                // nothing...
            }
        }

        return defaultValue;
    }

    private void Set(string option, string value)
    {
        if (option == null)
        {
            throw new ArgumentNullException(nameof(option));
        }

        if (_key != null)
        {
            if (value == null)
            {
                _key.DeleteValue(option);
            }
            else
            {
                _key.SetValue(option, value, RegistryValueKind.String);
            }
        }
    }

    public void Set<T>(string option, T value)
    {
        var content = JsonConvert.SerializeObject(value);
        Set(option, content);
    }
}
