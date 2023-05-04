import React from 'react';
import Lista from './Lista';
import Link from 'next/link';
export default function UltimasListas() {
  return (
    <section className='p-10'>
      <div className='flex justify-between border-b-2 px-3 mb-5'>
        <p>ULTIMAS LISTAS</p>
        <Link href={'/listas'}> VER TODAS</Link>
      </div>
      <div className='flex flex-wrap justify-evenly gap-5'>
        <Lista/>
        <Lista/>
        <Lista/>
        <Lista/>
      </div>
    </section>
  );
}