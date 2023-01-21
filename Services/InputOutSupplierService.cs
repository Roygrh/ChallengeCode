using ChallengeCode.DTO;
using System.IO;
using System.Text;

namespace ChallengeCode.Services{
    public class InputOutSupplierService{

    public List<string> ProvideInput(string path){

        List<string> lines = File.ReadAllLines(path, Encoding.UTF8).ToList();
        return lines;
    }

    public bool ProvideOutput(string path, List<string> lines){
        try{
            File.WriteAllLines(path, lines, Encoding.UTF8);
            return true;
        }
        catch (Exception e){
            return false;
        }
    }
}
}