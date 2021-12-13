echo off

set /p day=What number day should we add? 

echo "" > .\inputs\day%day%.txt
echo namespace Advent { internal class Day%day% : ISolution {} } > .\Day%day%.cs
