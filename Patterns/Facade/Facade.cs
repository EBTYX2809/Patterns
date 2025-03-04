namespace Patterns.Facade
{
    // Подсистема 1: Загрузка текстур
    class TextureManager
    {
        public void LoadTexture(string name)
        {
            Console.WriteLine($"Загрузка текстуры: {name}");
        }
    }

    // Подсистема 2: Загрузка звуков
    class SoundManager
    {
        public void LoadSound(string name)
        {
            Console.WriteLine($"Загрузка звука: {name}");
        }
    }

    // Подсистема 3: Загрузка 3D-моделей
    class ModelManager
    {
        public void LoadModel(string name)
        {
            Console.WriteLine($"Загрузка 3D-модели: {name}");
        }
    }

    // ФАСАД: Объединяет все менеджеры под единым интерфейсом
    class ResourceLoader
    {
        private TextureManager textureManager;
        private SoundManager soundManager;
        private ModelManager modelManager;

        public ResourceLoader()
        {
            textureManager = new TextureManager();
            soundManager = new SoundManager();
            modelManager = new ModelManager();
        }

        public void LoadAllResources()
        {
            textureManager.LoadTexture("hero.png");
            soundManager.LoadSound("battle_theme.mp3");
            modelManager.LoadModel("enemy.obj");
            Console.WriteLine("Все ресурсы загружены через фасад.");
        }
    }

    // Клиентский код
    public class Facade
    {
        static void Main()
        {
            ResourceLoader resourceLoader = new ResourceLoader();

            // Вместо того чтобы работать с кучей отдельных классов, вызываем один метод фасада
            resourceLoader.LoadAllResources();
        }
    }

}


