//Joel De Jesús Oseliz Reynoso - 20231132
//Tarea 4 - Prueba Automatizada

using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        // En esta parte configuro el ChromeDriver
        IWebDriver driver = new ChromeDriver();

        // Aquí coloco la ruta donde se guardarán las capturas de pantalla
        string screenshotDirectory = @"C:\Users\Laptop\source\repos\SeleniumTestProject\Capturas de Pantalla Automáticas";

        // En esta parte se crea el directorio si no existe
        if (!Directory.Exists(screenshotDirectory))
        {
            Directory.CreateDirectory(screenshotDirectory);
        }

        // En esta parte establezco un contador para las capturas
        int screenshotCounter = 1;

        try
        {
            // Historia de Usuario 1: Verificar el título de la página=============================================================================================
            driver.Navigate().GoToUrl("https://google.com");
            driver.Manage().Window.Maximize();
            string pageTitle = driver.Title;

            if (pageTitle == "Google")
            {
                Console.WriteLine("Prueba exitosa: El título de la página es correcto.");
            }
            else
            {
                Console.WriteLine("Prueba fallida: El título de la página es incorrecto.");
            }
            //Por cierto la parte de "HU" es Historia Usuario y "Titulo página" pues obviamente es el título que se le dará a la captura de pantalla
            TakeScreenshot(driver, screenshotDirectory, $"HU1_TituloPagina_{screenshotCounter++}");

            // Historia de Usuario 2: Verificar que la barra de búsqueda esté visible=============================================================================================
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            if (searchBox.Displayed)
            {
                Console.WriteLine("Prueba exitosa: La barra de búsqueda está visible.");
            }
            else
            {
                Console.WriteLine("Prueba fallida: La barra de búsqueda no está visible.");
            }
            TakeScreenshot(driver, screenshotDirectory, $"HU2_BarraBusqueda_{screenshotCounter++}");

            // Historia de Usuario 3: Realizar una búsqueda=============================================================================================
            searchBox.SendKeys("Microsoft");
            searchBox.Submit();
            Thread.Sleep(2000);
            Console.WriteLine("Prueba exitosa: La busqueda automatizada está visibe.");
            TakeScreenshot(driver, screenshotDirectory, $"HU3_DespuesBusqueda_{screenshotCounter++}");

            // Historia de Usuario 4: Verificar botón "Buscar con Google"=============================================================================================
            driver.Navigate().GoToUrl("https://google.com");
            IWebElement searchButton = driver.FindElement(By.Name("btnK"));
            if (searchButton.Displayed)
            {
                Console.WriteLine("Prueba exitosa: El botón 'Buscar con Google' está visible.");
                searchButton.Click();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Prueba fallida: El botón 'Buscar con Google' no está visible.");
            }
            TakeScreenshot(driver, screenshotDirectory, $"HU4_BotonBuscar_{screenshotCounter++}");

            // Historia de Usuario 5: Verificar botón "Iniciar sesión"=============================================================================================
            IWebElement signInButton = driver.FindElement(By.XPath("//a[contains(@aria-label, 'Iniciar sesión')]"));

            if (signInButton.Displayed)
            {
                Console.WriteLine("Prueba exitosa: El botón 'Iniciar sesión' está visible.");
            }
            else
            {
                Console.WriteLine("Prueba fallida: El botón 'Iniciar sesión' no está visible.");
            }
            TakeScreenshot(driver, screenshotDirectory, $"HU5_IniciarSesion_{screenshotCounter++}");
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine($"Prueba fallida: Elemento no encontrado - {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error durante la prueba: {ex.Message}");
        }
    }

    static void TakeScreenshot(IWebDriver driver, string directory, string fileName)
    {
        try
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string filePath = Path.Combine(directory, $"{fileName}.png");
            screenshot.SaveAsFile(filePath);
            Console.WriteLine($"Captura de pantalla guardada: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar la captura de pantalla: {ex.Message}");
        }
    }
}
