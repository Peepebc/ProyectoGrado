import Link from "next/link";
import Image from 'next/image';
import logo from '../../public/resources/moviebox.png';
import peliculas from '../../public/resources/peliculas.png';
import { useState } from "react";
import LayoutNoNav from "../components/LayoutNoNav";

Register.getLayout = function(page) {
    return <LayoutNoNav>{page}</LayoutNoNav>;
  };

export default function Register(){

    const [like,setLike] = useState(false)
    return (
        <div className="flex justify-center items-center min-h-screen py-5">
            <div className=" md:flex flex-row shadow-2xl shadow-black rounded-lg">
                <div className="rounded-lg p-10 md:pr-32" style={{backgroundColor: "#1F2E55"}}>
                    <Link href={'/'}>
                        <Image className="inline-block" src={logo} alt="logo"/>
                    </Link>
                    <h1 className="text-4xl py-5">REGISTER</h1>
                    <form action="" className="w-96">
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Usuario</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text" name="user" id="user" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="email">Email</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="email" name="email" id="email" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="nombre">Nombre</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="nombre" name="nombre" id="nombre" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="apellidos">Apellidos</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text" name="apellidos" id="apellidos" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="fechaNac">Fecha Nacimiento</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="date" name="fechaNac" id="fechaNac" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="pass">Contraseña</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="password" name="pass" id="pass" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="confPass">Confirmar Contraseña</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="password" name="confPass" id="confPass" required/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="pfp">Foto de Perfil</label>
                            <input className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="file" name="pfp" id="pfp" required/>
                        </div>
                        <div className="flex flex-col py-4">
                            <button className=" rounded-lg p-2 bg-gray-100 text-black">
                                REGISTER
                            </button>
                        </div>
                    </form>
                </div>
                <Image className="hidden md:block" src={peliculas} alt="peliculas" width={650} height={700}/>
            </div>
        </div>
    )
}
