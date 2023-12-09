window.AppConfig = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\\
<configuration>\\
  <!--\\
  <add key=\"HttpSvcUrl\" value=\"http://localhost/bioapp/service.svc\" />\\
  -->\\
  <add key=\"HttpSvcUrl\" value=\"http://www.bioappsystem.com/BIOAppMalay/Service.svc\" />\\
    <system.serviceModel>\\
        <bindings>\\
            <basicHttpBinding>\\
                <binding name=\"BasicHttpBinding_IService\" />\\
            </basicHttpBinding>\\
        </bindings>\\
        <client>\\
            <endpoint address=\"http://www.bioappsystem.com/BIOAppMalay/Service.svc\"\\
                binding=\"basicHttpBinding\" bindingConfiguration=\"BasicHttpBinding_IService\"\\
                contract=\"ServiceReference1.IService\" name=\"BasicHttpBinding_IService\" />\\
        </client>\\
    </system.serviceModel>\\
</configuration>";