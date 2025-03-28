# UnityWebRequest-Example

Version - Unity 2022.3.44.f1

#### Введение

Приложение состоящее из двух вкладок, которые можно переключить в нижней навигационной панели. Первая вкладка с погодой, вторая вкладка список пород собак. Вкладки должны переключатся. В приложении присутствует система взаимодействия с сервером, работающая посредством очереди исполняемых запросов(то есть, все запросы к серверу должны выполняться друг за другом, после завершения предыдущего запроса выполняется следующий в очереди). В очередь должна быть возможность добавлять запросы и получать результат выполненного запроса.

#### API

- Погода - https://api.weather.gov/gridpoints/TOP/32,81/forecast
- Породы собак - https://dogapi.dog/api/v2/breeds

#### Используемые технологии

- **Zenject**
- **UnityWebRequest**
- **Task**

#### Паттерны и архитектура

- **MVP - Passive View**
- **Zenject Factory**
- **Custom EventBus**
