import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { AiOutlineSearch } from 'react-icons/ai'
import BrandFilter from '../../components/BrandFilter'
import CardApprove from '../../components/CardApprove'
import CategoryFilter from '../../components/CategoryFilter'
import DateFilter from '../../components/DateFilter'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'
import { checkWord } from '../../util/checkWord'
import CarouselImage from '../../components/CarouselImage'
const readOnly = [
  true,
  false,
  false,
  false,
  false
  
]
const buttonActive = [true,false,false,true]
const types = [
  'string',
  'string',
  'number',
  'number',
  'string',
  'date',
  'more',
  'none',
  'none'
]
const initValues = {
  id : '',
  avatarUrl : '',
  username : "",
  email: '',
  
}
const lable = [
  "Name","Description","Pirce",'Stock',"Approve",'Create date',"More"
]

const actions = {
  getALl: '/Product/AdminGetAll',
  getRole: "/Roles/getRoleByUser",
  getById: "/Product/AdminGetById/",
  getProfile: '/user/profile',
  delete:'Product/delete/'
}

function Approve() {
  const [dataInfo,setDataInfo]  = useState([])
  const [infoActive,setInfoActive] = useState(false)
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [name,description,price,stock] = value
  const  valueAction = {name,description,price,stock}
  useEffect(()=> { 

      instance.get(actions.getALl)
      .then( rs => {
  
        let temp = new Array()
        
        rs.data.map(({id,name,description,price,stock,createdDate,categoryId,brandId,isHidden,status}) => {
  
  
          if (status == 2){
           temp.push({
            name,
            description,
            price,
            stock,
            isHidden: status == 2 ? false : true,
            createdDate,
            more: id,
            categoryId,
            brandId,
          })}
        })
        setData(temp)
      })
      .catch(e => console.log(e))
    

  },[reload])


  const setReloaded = () => setReload(!reload)
  const setDataIF =  value => {
    setDataInfo(value)
      instance.get(`${actions.getById}${value}`)
      .then(  rs => {setDataInfo(rs.data)
      })
  }
 
 
  const setInfo = () => setInfoActive(!infoActive)
  return (
    <>
    <div className="search-table">
     {infoActive && <CardApprove setReloaded={setReloaded} setInfo={setInfo} idInfo={dataInfo}>
      
     <CarouselImage idInfo  = {dataInfo}/>

      </CardApprove>}
      {/* <DateFilter />
      <CategoryFilter />
      <BrandFilter /> */}
    
      </div>
      <TableSearch lable={lable}   setDataInfo = {setDataIF} setInfo = {setInfo} buttonActive={buttonActive}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
    </>
  )
}

export default Approve
