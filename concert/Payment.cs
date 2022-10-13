public class Payment
{
    private string bankName,bankNumber,ownerName,creditNumber,dateCredit,cvv;

    public Payment(string bankName, string bankNumber)
    {
        this.bankName = bankName;
        this.bankNumber = bankNumber;
    }

    public Payment(string ownerName, string creditNumber, string dateCredit, string cvv)
    {
        this.ownerName = ownerName;
        this.creditNumber = creditNumber;
        this.dateCredit = dateCredit;
        this.cvv = cvv;
    }

    public string getBankName(){
        return this.bankName;
    }

    public string getBankNumber(){
        return this.bankNumber;
    }

    public string getOwnerName(){
        return this.ownerName;
    }

    public string getCreditNumber(){
        return this.creditNumber;
    }
    public string getDateCredit(){
        return this.dateCredit;
    }
    public string getCVV(){
        return this.cvv;
    }

    public bool isBank(){
        if(this.bankName != null){
            return true;
        }
        return false;
    }

}