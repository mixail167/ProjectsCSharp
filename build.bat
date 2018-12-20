@echo off

set PROGRAMFILES=Program Files (x86)
If %PROCESSOR_ARCHITECTURE%==x86 If Not Defined PROCESSOR_ARCHITEW6432 Set PROGRAMFILES=Program Files
set VS=C:\%PROGRAMFILES%\Microsoft Visual Studio 12.0\Common7\IDE
set pathToRep=D:\CSharp\Projects

echo %path% | Find /I /c "%VS%" > nul
if %ERRORLEVEL%==0 goto jump
set path=%path%;%VS%
:jump

echo Rebuild %pathToRep%\WinForms and Console\AlglibTest\AlglibTest.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\AlglibTest\AlglibTest.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\Algorithms\Algorithms.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\Algorithms\Algorithms.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\AudioPlayer\AudioPlayer.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\AudioPlayer\AudioPlayer.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\BootUSB\BootUSB.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\BootUSB\BootUSB.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\CalendarGenerator\CalendarGenerator.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\CalendarGenerator\CalendarGenerator.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\Clock\Clock.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\Clock\Clock.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\ClockWidget\ClockWidget.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\ClockWidget\ClockWidget.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\ColorSlider\ColorSlider.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\ColorSlider\ColorSlider.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\Config\Config.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\Config\Config.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\Game15\Game15.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\Game15\Game15.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\GifAnime\GifAnime.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\GifAnime\GifAnime.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\Jace\Jace.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\Jace\Jace.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\LocalizationApp\LocalizationApp.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\LocalizationApp\LocalizationApp.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\MathParser\MathParser.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\MathParser\MathParser.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\OpenGl2DApp\OpenGl2DApp.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\OpenGl2DApp\OpenGl2DApp.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\OpenGL3DApp\OpenGL3DApp.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\OpenGL3DApp\OpenGL3DApp.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\SAPIntegration\SAPIntegration.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\SAPIntegration\SAPIntegration.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\SnakeNew\SnakeNew.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\SnakeNew\SnakeNew.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\VideoPlayer\VideoPlayer.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\VideoPlayer\VideoPlayer.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\VKApi\VKApi.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\VKApi\VKApi.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\VKStealer\VKStealer.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\VKStealer\VKStealer.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\WaveAlgorithm\WaveAlgorithm.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\WaveAlgorithm\WaveAlgorithm.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\YouTubeDownloaderApp\YouTubeDownloaderApp.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\YouTubeDownloaderApp\YouTubeDownloaderApp.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\TCP\ChatClient\ChatClient.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\TCP\ChatClient\ChatClient.sln" /Rebuild

echo Rebuild %pathToRep%\WinForms and Console\TCP\ChatServer\ChatServer.sln
"%VS%\devenv.exe" "%pathToRep%\WinForms and Console\TCP\ChatServer\ChatServer.sln" /Rebuild

echo Rebuild %pathToRep%\WPF\Millionaire\Millionaire.sln
"%VS%\devenv.exe" "%pathToRep%\WPF\Millionaire\Millionaire.sln" /Rebuild

echo Rebuild %pathToRep%\WPF\MineSweeper\MineSweeper.sln
"%VS%\devenv.exe" "%pathToRep%\WPF\MineSweeper\MineSweeper.sln" /Rebuild

echo Rebuild %pathToRep%\OtherDevelopments\Asteroids\Naves.sln
"%VS%\devenv.exe" "%pathToRep%\OtherDevelopments\Asteroids\Naves.sln" /Rebuild

echo Rebuild %pathToRep%\OtherDevelopments\BackpackTask\BackpackTask.sln
"%VS%\devenv.exe" "%pathToRep%\OtherDevelopments\BackpackTask\BackpackTask.sln" /Rebuild

echo Rebuild %pathToRep%\OtherDevelopments\GamehacklabTrainerEngine\GamehacklabTrainerEngine.sln
"%VS%\devenv.exe" "%pathToRep%\OtherDevelopments\GamehacklabTrainerEngine\GamehacklabTrainerEngine.sln" /Rebuild

echo Rebuild %pathToRep%\OtherDevelopments\Perceptron\Perceptron.sln
"%VS%\devenv.exe" "%pathToRep%\OtherDevelopments\Perceptron\Perceptron.sln" /Rebuild

echo Rebuild %pathToRep%\OtherDevelopments\VideoServer\VideoServer.sln
"%VS%\devenv.exe" "%pathToRep%\OtherDevelopments\VideoServer\VideoServer.sln" /Rebuild