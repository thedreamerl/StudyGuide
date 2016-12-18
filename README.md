# StudyGuide
WPF-application
После клонирования репозитория на компьютер надо произвести следующие действия:
1) Если на компьютере не установлен sql server, то потребуется поменять название сервера на (localdb)\mssqllocaldb. 
Если же sql server установлен, то имя локального сервера можно узнать, например, указанным здесь способом: http://stackoverflow.com/questions/16088151/how-to-find-server-name-of-sql-server-management-studio
2) Если база данных с именем StudyGuideDB уже существует, то рекомендуется переименовать базу данных
3) Нужно восстановить пакеты nuget, выполнив сборку проекта
4) Перед запуском проекта нужно выбрать StudyGuide.UI запускаемым проектом
5) в Package Manager Console необходимо выполнить Update-Database, выбрав проект по умолчанию StudyGuide.DataBase (обязательно выполнив пункт 4)
Если произошла ошибка, значит скорее всего неправильно было указано имя локального сервера
