
DefaultJsonFileSettingsProvider:默认的配置文件提供器。
	1、可以实现配置的读取和更新。
	2、监视配置文件改变。
	3、可以通过Factory工厂类创建。
	4、对配置进行缓存。当监视到配置文件发生变化时清空缓存。
	事件：
		1、ClearSettingsWhenFileChange当检测到配置文件发生变化，在清空缓存配置之前发生。
		2、ReadSettingsFile当读取本地配置文件更新本地缓存配置之后发生。

Factory：工厂类，用于创建默认配置文件提供器

IGetSettins：泛型接口。实现特定类型的配置读取。

ISaveSettings：泛型接口。实现特定类型的配置保存。

ISettings：泛型接口。实现特定类型配置的保存和读取。