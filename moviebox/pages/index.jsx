
import { useContext } from 'react'
import Context from './Context.js'
import UltimasPeliculas from './components/UltimasPeliculas.jsx'
import UltimasResenas from './components/UltimasResenas.jsx'
import UltimasListas from './components/UltimasListas.jsx'

export default function Home() {

  const {user, setUser} = useContext(Context)


  return (
    <>
      <UltimasPeliculas/>
      <UltimasResenas/>
      <UltimasListas/>
    </>
  )
}
