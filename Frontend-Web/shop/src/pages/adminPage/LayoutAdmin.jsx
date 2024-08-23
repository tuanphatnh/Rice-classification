import React from 'react'
import { useState } from 'react'
import { Outlet } from 'react-router-dom'
import SearchBar from '../../components/SearchBar'
import SideBar from '../../components/SideBar'

function LayoutAdmin() {
  const [expand,setExpand] = useState(false)
  const handleChangeExpand = () => {
    setExpand(!expand)
  }
  return (
    <div className='container-abc'>
       <SearchBar expand={expand} />
        <SideBar expand={expand} handleChangeExpand={handleChangeExpand}/>
        <div className={ !expand ? 'content-abc' : 'content-abc expand'}>
            <Outlet/>
        </div>
    </div>
  )
}

export default LayoutAdmin
