public class Users
{
    private string prefixName, firstName, lastName, email, password,studentId;
    private int age;

    public Users(string prefixName, string firstName, string lastName, string email, string password, int age)
    {
        this.prefixName = prefixName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.age = age;
    }
    public Users(string prefixName, string firstName, string lastName, string email, string password, int age, string studentId)
    {
        this.prefixName = prefixName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.age = age;
        this.studentId = studentId;
    }

    public string getEmail()
    {
        return this.email;
    }

    public string getPassword()
    {
        return this.password;
    }

    public string getPrefix()
    {
        return this.prefixName;
    }

    public string getFirstName()
    {
        return this.firstName;
    }

    public string getLastName()
    {
        return this.lastName;
    }

    public string getStudentId()
    {
        return this.studentId;
    }

}