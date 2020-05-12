using System.Linq;
using System.Xml.Linq;
using System.IO;

int Main(string[] args){
    
    const int FAIL_RESULT = 1;
    const int SUCCESS_RESULT = 0;
    var coverageFilePath = new FileInfo(args[1]);
    var coverage = new TestCoverageChecker(coverageFilePath);

    var requiredCoverage = float.Parse(args[0]);
    var actualCoverage = coverage.GetCodeCoverage();
    if (actualCoverage >= requiredCoverage) {
        Console.WriteLine($"Code coverage checks pass with {actualCoverage}/{requiredCoverage}%.");
        return SUCCESS_RESULT;
    } else {
        Console.Error.WriteLine($"Insufficient code coverage {actualCoverage}/{requiredCoverage}%");
        return FAIL_RESULT;        
    }
}

class TestCoverageChecker {

    private readonly FileInfo _coverageSummaryFile;
    private readonly XDocument _coverageSummaryReport;

    public TestCoverageChecker(FileInfo coverageSummaryFile){

        if( !coverageSummaryFile.Exists )
            throw new FileNotFoundException(coverageSummaryFile.FullName);

        Console.WriteLine("Found coverage file at " + coverageSummaryFile.FullName);
        var coverageFileText = File.ReadAllText(coverageSummaryFile.FullName);
        _coverageSummaryReport = XDocument.Parse(coverageFileText);

        _coverageSummaryFile = coverageSummaryFile;
    }

    public float GetCodeCoverage(){
        if( !_coverageSummaryFile.Exists )
            throw new FileNotFoundException(_coverageSummaryFile.FullName);

        var coverage = _coverageSummaryReport.Descendants("Linecoverage");
        if(!coverage.Any()) return 0;
        
        var coverageStr = coverage.First().Value;
        return float.Parse(coverageStr);
    }

}

class InsufficientCodeCoverageException : Exception {

    private readonly float _actualCoverage;
    private readonly float _requiredCoverage;
    public override string Message => $"Code checks failed with {_actualCoverage}/{_requiredCoverage}%.";

    public InsufficientCodeCoverageException( float actualCoverage, float requiredCoverage){
        _actualCoverage = actualCoverage;
        _requiredCoverage = requiredCoverage;
    }
}

Console.WriteLine($"Attempting to assert that code coverage exceeds {Args[0]}% in {Args[1]}.");
Main(Args.ToArray());