using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    /// <summary>
    /// Статус сортировки
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Данные обновлены или только что инициализированы
        /// Необходимо провести сортировку
        /// </summary>
        Initial,

        /// <summary>
        /// В произвольном формате (по возрастанию идентификаторов)
        /// </summary>
        Unsorted,

        /// <summary>
        /// По возрастанию
        /// </summary>
        Ascending,

        /// <summary>
        /// По убыванию
        /// </summary>
        Descending
    }

    /// <summary>
    /// Контейнер данных списка карт для сериализации
    /// </summary>
    public class TasklistData : UniqueData
    {
        public bool IsEssential;
        public List<TaskData> List;
        public float CompletionPercentage;
        public int PositionInWeek;
    }

    /// <summary>
    /// Список карт
    /// </summary>
    public class Tasklist : Unique<Tasklist>, IUniqueIdentifiable
    {
        /// <summary>
        /// Создать объект из контейнера с данными
        /// </summary>
        /// <param name="data">Контейнер с данными</param>
        /// <returns>Объект</returns>
        public static Tasklist FromData(TasklistData data)
        {
            List<Task> list = new List<Task>();

            foreach (TaskData cardData in data.List)
            {
                list.Add(Task.FromData(cardData));
            }

            return new Tasklist(data.Id, data.Name, data.IsEssential, list,data.PositionInWeek);
        }

        //TODO: убрать из есеншиал. Все такие тасклисты и так эсеншиал :)
        /// <summary>
        /// Статус закрепления
        /// </summary>
        private bool _isEssential;

        private float _completionPercentage;

        private int _positionInWeek;

        /// <summary>
        /// Хранилище карт
        /// </summary>
        private readonly Dictionary<int, Task> _pool = new Dictionary<int, Task>();

        /// <summary>
        /// Отсортированный по дате обновления
        /// список идентификаторов карт
        /// (только невыполненные обычные)
        /// </summary>
        private readonly List<int> _idListDefault = new List<int>();

        /// <summary>
        /// Отсортированный по важности и дате обновления
        /// список идентификаторов карт
        /// (только невыполненные важные)
        /// </summary>
        private readonly List<int> _idListImportant = new List<int>();

        /// <summary>
        /// Отсортированный по статусу выполнения и дате обновления
        /// список идентификаторов карт
        /// (только выполненные)
        /// </summary>
        private readonly List<int> _idListCompleted = new List<int>();

        private readonly List<int> _idPositionInWeek = new List<int>();

        /// <summary>
        /// Статус сортировки
        /// </summary>
        private SortOrder _status = SortOrder.Initial;

        /// <summary>
        /// Конструктор списка из заданного списка карт
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="name">Название</param>
        /// <param name="isEssential">Статус закрепления</param>
        /// <param name="list">Список карт</param>
        public Tasklist(int id, string name, bool isEssential, List<Task> list) : this(id, name, isEssential)
        {
            foreach (Task task in list)
            {
                Set(task);
            }
        }
        //TODO: создать процентное соотношение всех выполненых задач в таск листе по отношению ко всем задачам по полю iscompelited
        public float Percent()
        {
            float percent = 0;
            int sum = 0;
            foreach(KeyValuePair<int,Task> pair in All)
            {
                if (pair.Value.IsCompleted)
                {
                    sum++;
                }
            }
            percent = (sum * 100) / All.Count; 
            return percent;
        }
        public Tasklist(int id, string name, bool isEssential, List<Task> list, int weekPosition) : this(id, name, isEssential,list)
        {
            PositionInWeek = weekPosition;
        }
        //TODO: конструктор по викпозишену

        /// <summary>
        /// Конструктор пустого списка
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="name">Название</param>
        /// <param name="isEssential">Статус закрепления</param>
        /// <exception cref="ArgumentException"></exception>
        public Tasklist(int id, string name, bool isEssential = false) : base(id, name)
        {
            IsEssential = isEssential;
        }

        /// <summary>
        /// Базовый конструктор с автоматическим определением идентификатора
        /// </summary>
        /// <param name="name">Название</param>
        public Tasklist(string name) : base(name)
        {
            // PASS
        }

        /// <summary>
        /// Получить контейнер с данными из объекта
        /// </summary>
        /// <returns>Контейнер с данными</returns>
        public TasklistData ToData()
        {
            List<TaskData> list = new List<TaskData>();

            foreach (KeyValuePair<int, Task> item in All)
            {
                list.Add(item.Value.ToData());
            }

            return new TasklistData
            {
                Id = Id,
                Name = Name,
                IsEssential = IsEssential,
                List = list,
                PositionInWeek = PositionInWeek,
                CompletionPercentage = CompletionPercentage
            };
        }

        /// <summary>
        /// Доступ к статусу закрепления
        /// </summary>
        public bool IsEssential
        {
            get => _isEssential;
            private set => _isEssential = value;
        }
        public int PositionInWeek
        {
            get => _positionInWeek;
            private set => _positionInWeek = value;
        }

        public float CompletionPercentage
        {
            get => _completionPercentage;
            private set => _completionPercentage = value;
        }

        /// <summary>
        /// Доступ к хранилищу карт
        /// </summary>
        public Dictionary<int, Task> All => _pool;

        /// <summary>
        /// Доступ к статусу сортировки
        /// </summary>
        public SortOrder Status
        {
            get => _status;
            private set => _status = value;
        }

        /// <summary>
        /// Получить карту по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор карты</param>
        /// <returns>Объект карты</returns>
        public Task Get(int id)
        {
            return All[id];
        }

        /// <summary>
        /// Получить отсортированный
        /// по дате обновления список карт
        /// (только невыполненные обычные)
        /// </summary>
        /// <param name="order">Статус сортировки</param>
        /// <returns>Список карт</returns>


        /// <summary>
        /// Получить отсортированный по важности
        /// и дате обновления список карт
        /// (только невыполненные важные)
        /// </summary>
        /// <param name="order">Статус сортировки</param>
        /// <returns>Список карт</returns>


        /// <summary>
        /// Получить отсортированный по статусу выполнения
        /// и дате обновления список карт
        /// (только выполненные)
        /// </summary>
        /// <param name="order">Статус сортировки</param>
        /// <returns>Список карт</returns>

        public List<Task> GetListCompleted(SortOrder order = SortOrder.Descending)
        {
            return GetListSorted(_idListCompleted, order);
        }
        /// <summary>
        /// Получить список карт по части названия или описания
        /// </summary>
        /// <param name="content">Часть названия или описания карты</param>
        /// <returns>Список карт</returns>
        public List<Task> SearchByContent(string content)
        {
            content = content.Trim().ToLower();

            List<Task> result = new List<Task>();

            foreach (KeyValuePair<int, Task> item in All)
            {
                if (!item.Value.Name.ToLower().Contains(content))
                {
                    if (!item.Value.Description.ToLower().Contains(content))
                    {
                        continue;
                    }
                }

                result.Add(item.Value);
            }

            return result;
        }

        /// <summary>
        /// Получить список карт по дате обновления без учета времени
        /// </summary>
        /// <param name="date">Дата обновления</param>
        /// <returns>Список карт</returns>
        public List<Task> SearchByDateUpdated(DateTime date)
        {
            // Поиск карт, обновленных в определенный день указанной даты
            DateTime min = new DateTime(date.Year, date.Month, date.Day);
            DateTime max = min.AddHours(23).AddMinutes(59);

            return SearchByDateUpdated(min, max);
        }

        /// <summary>
        /// Получить список карт по дате обновления в заданном промежутке
        /// </summary>
        /// <param name="minDate">Начало</param>
        /// <param name="maxDate">Конец</param>
        /// <returns>Список карт</returns>
        public List<Task> SearchByDateUpdated(DateTime minDate, DateTime maxDate)
        {
            List<Task> result = new List<Task>();

            foreach (KeyValuePair<int, Task> item in All)
            {
                if (minDate > item.Value.Date.Updated)
                {
                    continue;
                }

                if (maxDate < item.Value.Date.Updated)
                {
                    continue;
                }

                result.Add(item.Value);
            }

            return result;
        }

        /// <summary>
        /// Сохранить карту
        /// </summary>
        /// <param name="card">Объект карты</param>
        public void Set(Task task)
        {
            Status = SortOrder.Initial;
            All[task.Id] = task;
        }

        /// <summary>
        /// Удалить карту с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор карты</param>
        /// <returns>Статус успеха удаления</returns>
        public bool Remove(int id)
        {
            Status = SortOrder.Initial;

            return All.Remove(id);
        }

        /// <summary>
        /// Проверить, существует ли карта с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Статус проверки</returns>
        public bool Contains(int id)
        {
            return All.ContainsKey(id);
        }

        /// <summary>
        /// Получить отсортированный список карт
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="order">Статус сортировки</param>
        /// <returns>Список карт</returns>
        private List<Task> GetListSorted(List<int> source, SortOrder order = SortOrder.Ascending)
        {
            Sort(order);

            List<Task> result = new List<Task>();

            foreach (int id in source)
            {
                result.Add(Get(id));
            }

            return result;
        }

        /// <summary>
        /// Выбрать подходящий источник для указанного направления сортировки
        /// </summary>
        /// <param name="order">Статус сортировки</param>
        /// <returns>Источник</returns>
        private IEnumerable<KeyValuePair<int, Task>> Source(SortOrder order)
        {
            if (order == SortOrder.Unsorted)
            {
                return All;
            }

            if (order == SortOrder.Ascending)
            {
                return All.OrderBy(item => item.Value.Date.Updated ?? item.Value.Date.Created);
            }

            return All.OrderByDescending(item => item.Value.Date.Updated ?? item.Value.Date.Created);
        }

        /// <summary>
        /// Определить идентификатор карты в один из списков для сортировки
        /// </summary>
        /// <param name="card">Карта</param>
        private void Cache(Task task)
        {
            _idListDefault.Add(task.Id);
        }

        /// <summary>
        /// Отсортировать списки идентификаторов
        /// </summary>
        /// <param name="order">Статус сортировки</param>
        private void Sort(SortOrder order)
        {
            // Если списки уже отсортированы в указанном формате
            if (order == Status)
            {
                return;
            }

            _idListDefault.Clear();
            _idListImportant.Clear();
            _idListCompleted.Clear();
            _idPositionInWeek.Clear();

            foreach (KeyValuePair<int, Task> item in Source(order))
            {
                Cache(item.Value);
            }

            Status = order;
        }

        public bool Equals(Tasklist list)
        {
            return base.Equals(list) && list.IsEssential == IsEssential;
        }

        public override string ToString()
        {
            return ToData().ToString();
        }
    }
}
