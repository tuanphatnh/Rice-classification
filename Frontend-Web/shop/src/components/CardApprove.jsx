import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { instance } from '../config/axiosConfig';


function CardApprove(props) {
  const {setInfo,userId,children,idInfo,report,productId, setReload} = props
  const [id,setId] = useState(idInfo)
  const [product,setProduct] = useState({})
  const [brand,setBrand] = useState("")
  const [size,setSize] = useState("")
  const [color,setColor] = useState("")
  const [category,setCategory] = useState("")
  const [user,setUser] = useState({})

  useEffect(()=>{

    instance.get(`/Product/AdminGetById/${idInfo}`)
    .then( rs => {setProduct(rs.data.product)
      setBrand(rs.data.product.brand.name)
      setCategory(rs.data.product.category.name)
      setColor(rs.data.product.color.hexValue)
      setSize(rs.data.product.size.sizeName)
      setUser(rs.data.user)
    })
},[])
const handleAprrove = () =>{

  if (report !==true ){
  instance.post(`/Product/ApproveProduct/${id}`)
  .then ( rs => {
    if (rs.data.isSuccess)
    {
        toast.success(rs.data.message)
        setInfo()
        setReload()
    }
    else{
        toast.error(rs.data.errors)
    }
  })
  .catch( e => console.log(e))}
  else{
    console.log(userId);
    instance.post(`/User/ban?userId=${userId}`)
    .then(rs => { if (rs.data.isSuccess)
      {
          toast.success(rs.data.message)
          setInfo()
          setReload()
      }
      else{
          toast.error(rs.data.errors)
      }
    })
    .catch( e => console.log(e))}
  }

const handleDeny = () => {
  if (report !== true){
  instance.delete(`/Product/delete/${id}`)
  .then ( rs => {
    if (rs.data.isSuccess)
    {
        toast.success(rs.data.message)
        setInfo()
        setReload()
    }
    else{
        toast.error(rs.data.message)
    }
  })
  .catch( e => console.log(e))}
  else{
    instance.get(`/PostAndPackage/hideProduct/${productId}`)
    .then ( rs => {
      if (rs.data.isSuccess)
      {
          toast.success(rs.data.message)
          setInfo()
          setReload()
      }
      else{
          toast.error(rs.data.message)
      }
    })
    .catch( e => console.log(e))}
  }

  return (
    <>
      <ToastContainer
        position="top-center"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
      <div class="card-abc">
        <button onClick={setInfo} type="button" class="dismiss">
          ×
        </button>
        <div class="header">
          {/* <div class="image">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24">
              <g stroke-width="0" id="SVGRepo_bgCarrier"></g>
              <g
                stroke-linejoin="round"
                stroke-linecap="round"
                id="SVGRepo_tracerCarrier"></g>
              <g id="SVGRepo_iconCarrier">
                {' '}
                <path
                  stroke-linejoin="round"
                  stroke-linecap="round"
                  stroke-width="1.5"
                  stroke="#000000"
                  d="M20 7L9.00004 18L3.99994 13"></path>{' '}
              </g>
            </svg>
          </div> */}
         <div class="card-content">
            {children}
            {report !== true &&
            <div>
            <span class="title">{product.name}</span>
            <p class="message">
            <span>User :</span> <span>{user.username}</span>
              <span>Description :</span> <span>{product.description}</span>
              <span> Price :</span> <span>{Intl.NumberFormat('vi-VN',{style:'currency',currency:'VND'}).format(product.price)} </span>
              <span>Category :</span> <span>{category}</span>
              <span>Size :</span> <span>{size}</span>
              <span>Brand :</span> <span>{brand}</span>
              <span>Color :</span> <span style={{display:'inline-block',backgroundColor:color,width:'20px',height: '20px',borderRadius:'50%'}}></span>

              
            </p></div>}
          </div>
          <div class="actions">
            <button type="button" onClick={()=>handleAprrove()} class="approve">
              { report ? "Ban user" :"Approve"}
            </button>
            <button type="button" onClick={()=> handleDeny()} class="deny">
              {report? "Hide product" :"Deny"}
            </button>
          </div>
        </div>
      </div>
    </>
  );
}

export default CardApprove;
