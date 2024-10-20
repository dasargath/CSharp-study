namespace Collections;

public class OtusDictionary
{
    private int[] _keys;
    private string[] _values;
    private int _count;
    private int _size = 8;

    public OtusDictionary()
    {
        _keys = new int [ _size ];
        _values = new string[ _size ];
        _count = 0;
    }
    
    public void Add(int key, string value)
    {
        switch (value)
        {
            case null:
                throw new ArgumentNullException(nameof(value), "Значение не может быть null.");
            case "":
                throw new ArgumentException("Значение не может быть пустым.", nameof(value));
            default:
            {
                if (Array.Exists(_values, val => val == value))
                {
                    throw new ArgumentException($"Значение {value} в словаре уже существует.");
                }
                break;
            }
        }

        if (_count == _size)
        {
            Resize();
        }
        
        int index = key % _size;

        while (_values[index] != null)
        {
            if (_count == _size)
            {
                throw new ArgumentException($"Такой ключ {key} уже существует.");
            }
            index = (index + 1) % _size;
        }
        _keys[index] = key;
        _values[index] = value;
        _count++;
    }
    
    
    public string Get(int key)
    {
        int index = key % _size;

        while (_values[index] != null)
        {
            if(_keys[index] == key)
                return _values[index];
            index = (index + 1) % _size;
        }
        Console.WriteLine($"Ошибка. Значение с ключом \"{key}\" не существует.");
        return null!;
    }

    
    public string this[int key]
    {
        get => Get(key);
        set => Add(key, value);
    }
    
    
    private void Resize()
    {
        _size = Math.Min( _size * 2, _count * 2 );
        int[] previousKeys = _keys;
        string[] previousValues = _values;
        
        _keys = new int[ _size ];
        _values = new string[ _size ];
        _count = 0;

        for (int i = 0; i < previousKeys.Length; i++)
        {
            if (previousValues[i] != null)
            {
                Add(previousKeys[i], previousValues[i]);
            }
        }
    }
}
