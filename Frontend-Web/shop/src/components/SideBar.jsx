import React from 'react'
import {  AiOutlineExpandAlt, AiOutlineLine } from 'react-icons/ai'
import linkAdmin from '../pages/adminPage/links'
import Logo from './Logo'
import NavList from './NavList'

function SideBar({expand, handleChangeExpand}) {
  return (
    <div className={expand  ?'sidebar small' : 'sidebar'}>
        <span className='small-button'>{ !expand ? <AiOutlineLine onClick={handleChangeExpand} /> : <AiOutlineExpandAlt onClick={handleChangeExpand} />}</span>
        <Logo expand={expand} />
        <NavList expand={expand} links = {linkAdmin}/>
    </div>
  )
}

export default SideBar
