import Link from "next/link";
import LayoutNoNav from "../components/LayoutNoNav";

ForgotPassword.getLayout = function(page) {
    return <LayoutNoNav>{page}</LayoutNoNav>;
  };

export default function ForgotPassword({data}){
    return (
        <div>
            <img src="" alt="" />
            <div className="flex justify-center align-middle items-center min-h-screen">
                <div className="rounded-lg p-10 shadow-2xl shadow-black" style={{backgroundColor: "#1F2E55"}}>
                    <form action="" className="w-72">
                        <h1 className="text-4xl text-center">LOGIN</h1>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Usuario</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text" name="user" id="user"/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="pass">Contraseña</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="password" name="pass" id="pass"/>
                        </div>
                        <div className="flex py-4 gap-2">
                            <input className="w-5 rounded-xl"  type="checkbox" name="remember" id="remember"/>
                            <label htmlFor="pass">Recuerdame</label>
                        </div>
                        <div className="flex flex-row justify-between py-4">
                            <button className="w-32 rounded-lg p-2 bg-gray-100 text-black">
                                LOGIN
                            </button>
                            <button className="w-32 rounded-lg p-2 bg-gray-100 text-black">
                                REGISTER
                            </button>
                        </div>
                        <Link href={'/forgotPassword'}>
                            ¿Has olvidado la contraseña?
                        </Link>
                    </form>
                </div>
            </div>
        </div>
    )
}
