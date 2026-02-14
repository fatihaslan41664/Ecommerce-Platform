using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}
//email
//:
//"fatih.fe205@gmail.com"
//firstName
//:
//"fatih"
//id
//:
//"112476262158539263391"
//idToken
//:
//"eyJhbGciOiJSUzI1NiIsImtpZCI6ImI1ZTQ0MGFlOTQxZTk5ODFlZTJmYTEzNzZkNDJjNDZkNzMxZGVlM2YiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIzNDIyMTEwMzcwNDMtNnQxc243bjc4MXJnZzM2bHQzdmMzazNmbzA1ZnVpbTEuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIzNDIyMTEwMzcwNDMtNnQxc243bjc4MXJnZzM2bHQzdmMzazNmbzA1ZnVpbTEuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTI0NzYyNjIxNTg1MzkyNjMzOTEiLCJlbWFpbCI6ImZhdGloLmZlMjA1QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYmYiOjE3NjIyNjIzOTYsIm5hbWUiOiJmYXRpaCDDh2VsZWJpIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hL0FDZzhvY0xabTVZVWZ1ZF9vcnhYYTdETzlTOGQybUFtUmxJM3A1aWJVa3h4V0s4eldPd0gxZz1zOTYtYyIsImdpdmVuX25hbWUiOiJmYXRpaCIsImZhbWlseV9uYW1lIjoiw4dlbGViaSIsImlhdCI6MTc2MjI2MjY5NiwiZXhwIjoxNzYyMjY2Mjk2LCJqdGkiOiJlNjg5MTYyOGQzYzljMGMxYmI3NGVlMWFmNjhkMmM4NTdhMjJjNDlmIn0.dPhIblLC-2-1WEMQJUfDkD3A4rNw47HpyZ_HROYSN5wUUmbbsDWW02R_XyeFth4I_urMGfxN3ccpRrQCek1q6VXyU_r9xbCq9F4oW2rM_pGFV6SR3RblPg0oSz-t7QWtXFX_11VeqIgVMJ08VvNsDwt_WpZR-3eNXSx7z8dTXLMkxLqy9N0zDVnO617_TB64JMfuPjN3oCgKijUSJ24ohOPF5o7wFdsZBDuRPECea1IT1ruM2_i9cbemL8xHcmVi5zXPwOe675ar0gy1Kq5VGFV7bklz4GfIbV8Mv4HPx1Dtjnw9dqoC6ydY4KSwPuVLJezsdhxZZ30DJ5Rsc2RGuQ"
//lastName
//:
//"Çelebi"
//name
//:
//"fatih Çelebi"
//photoUrl
//:
//"https://lh3.googleusercontent.com/a/ACg8ocLZm5YUfud_orxXa7DO9S8d2mAmRlI3p5ibUkxxWK8zWOwH1g=s96-c"
//provider
//:
//"GOOGLE"