public class Reserve
{
    private string seatList;
    private bool payed = false;
    private bool cancaled = false;
    private double totalAmount = 0;
    private Users userReserve;
    private Payment payment;

    public Reserve(Users userReserve, string seatList, double totalAmount)
    {
        this.userReserve = userReserve;
        this.seatList = seatList;
        this.totalAmount = totalAmount;
    }

    public string getSeatList()
    {
        return this.seatList;
    }

    public Users getUserReserve()
    {
        return this.userReserve;
    }

    public double getTotalAmount()
    {
        return this.totalAmount;
    }

    public Payment getPayment()
    {
        return this.payment;
    }

    

    public bool checkUserReserve(Users users)
    {
        if(users != null && !payed && !cancaled && users.getFirstName() == this.userReserve.getFirstName()){
            return true;
        }
        return false;
    }

    public bool checkReserve(Users users)
    {
        if(users != null && payed && !cancaled && users.getFirstName() == this.userReserve.getFirstName()){
            return true;
        }
        return false;
    }


    public void setPayed()
    {
        this.payed = true;
    }

    public void setPayment(Payment payment)
    {
        this.payment = payment;
    }

    public void setCancaled()
    {
        this.cancaled = true;
    }

}