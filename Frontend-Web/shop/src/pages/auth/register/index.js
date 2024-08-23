import classNames from 'classnames/bind';
import styles from './Register.module.scss';

import { useState, useRef, useEffect } from 'react';
import { Link, json } from 'react-router-dom';
import { EyeOutlined, EyeInvisibleOutlined } from '@ant-design/icons';
import { Checkbox, message } from 'antd';
import axios, { formToJSON } from 'axios';
import Loading from '~/components/Global/Loading';

const cx = classNames.bind(styles);

function Register() {
    const [messageApi, contextHolder] = message.useMessage();
    // hook state
    const [isLoader, setIsLoader] = useState(false);
    const [isPassword, setIsPw] = useState(false);
    const [isConfirmPw, setIsConfirmPw] = useState(false);
    const [userName, setUsername] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPW, setConfirmPW] = useState('');
    const [checkbox, setCheckBox] = useState(true);

    // hook ref
    const userNameRef = useRef(null);
    const emailRef = useRef(null);
    const passwordRef = useRef(null);
    const confirmPWRef = useRef(null);
    const checkBoxRef = useRef(null);
    const logEmail = useRef(null);
    const loguserName = useRef(null);
    const logPassword = useRef(null);
    const logConfirmPw = useRef(null);

    const validator = () => {
        if (!userName) {
            userNameRef.current.focus();
            loguserName.current.style.display = 'inline';
            return false;
        } else {
            loguserName.current.style.display = 'none';
        }

        if (!validateEmail(email)) {
            emailRef.current.focus();
            logEmail.current.style.display = 'inline';
            return false;
        } else {
            logEmail.current.style.display = 'none';
        }

        if (!password) {
            passwordRef.current.focus();
            logPassword.current.style.display = 'inline';
            return false;
        } else {
            logPassword.current.style.display = 'none';
        }

        if (password.length < 6) {
            passwordRef.current.focus();
            logPassword.current.style.display = 'inline';
            logPassword.current.innerHTML = 'Your password is shorter than 6 characters';
            return false;
        } else {
            logPassword.current.style.display = 'none';
        }

        if (confirmPW !== password) {
            confirmPWRef.current.focus();
            logConfirmPw.current.style.display = 'inline';
            return false;
        } else {
            logConfirmPw.current.style.display = 'none';
        }

        return true;
    };

    const validateEmail = (email) => {
        return email.match(
            /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        );
    };

    const submitRegister = () => {
        if (validator()) {
            setUsername('');
            setEmail('');
            setPassword('');
            setConfirmPW('');
            register();
        }
    };

    const register = () => {
        setIsLoader(true);
        const formData = {
            username: userName,
            email: email,
            password: password,
            confirmPassword: confirmPW,
        };
        axios
            .post('https://localhost:44352/api/Auth/register', JSON.stringify(formData), {
                headers: {
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    message.success('Successful Registration');
                    setIsLoader(false);
                }
            })
            .catch((err) => {
                if (err.response.status === 400) {
                    err.response.data.errors.forEach((err) => {
                        message.error(err);
                    });
                    console.log(err);
                    setIsLoader(false);
                }
            });
    };
    return (
        <div className={cx('wrap')}>
            <div className={cx('box')}>
                <h1 className="text-center text-4xl">Register</h1>
                <div className="flex justify-around">
                    <div className={cx('group')}>
                        <div className={cx('item')}>
                            <input
                                ref={userNameRef}
                                value={userName}
                                onChange={(e) => setUsername(e.target.value)}
                                type="text"
                                placeholder="Enter your username"
                                onKeyDown={(e) => e.key === 'Enter' && submitRegister()}
                            />
                            <span ref={loguserName} className={cx('item-log')} style={{ display: 'none' }}>
                                Enter username
                            </span>
                        </div>
                        <div className={cx('item')}>
                            <input
                                ref={emailRef}
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                type="email"
                                onKeyDown={(e) => e.key === 'Enter' && submitRegister()}
                                placeholder="Enter your email"
                            />
                            <span ref={logEmail} className={cx('item-log')} style={{ display: 'none' }}>
                                Please enter a valid email address
                            </span>
                        </div>
                        <div className={cx('item', 'password')}>
                            <input
                                className={cx('input-password')}
                                ref={passwordRef}
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                type={isPassword ? 'text' : 'password'}
                                onKeyDown={(e) => e.key === 'Enter' && submitRegister()}
                                placeholder="Password"
                            />
                            <div className={cx('icon')}>
                                {isPassword ? (
                                    <EyeOutlined onClick={() => setIsPw(false)} />
                                ) : (
                                    <EyeInvisibleOutlined onClick={() => setIsPw(true)} />
                                )}
                            </div>
                            <span ref={logPassword} className={cx('item-log')} style={{ display: 'none' }}>
                                Please enter a password
                            </span>
                        </div>
                        <div className={cx('item', 'password')}>
                            <input
                                className={cx('input-password')}
                                ref={confirmPWRef}
                                value={confirmPW}
                                onChange={(e) => setConfirmPW(e.target.value)}
                                type={isConfirmPw ? 'text' : 'password'}
                                onKeyDown={(e) => e.key === 'Enter' && submitRegister()}
                                placeholder="Confirm password"
                            />
                            <div className={cx('icon')}>
                                {isConfirmPw ? (
                                    <EyeOutlined onClick={() => setIsConfirmPw(false)} />
                                ) : (
                                    <EyeInvisibleOutlined onClick={() => setIsConfirmPw(true)} />
                                )}
                            </div>
                            <span ref={logConfirmPw} className={cx('item-log')} style={{ display: 'none' }}>
                                Password confirm does not match
                            </span>
                        </div>
                        <div className={cx('item')}>
                            <Checkbox ref={checkBoxRef} checked={checkbox} onClick={() => setCheckBox(!checkbox)}>
                                <p>
                                    I agree all statements in <a href="/123">Terms of service</a>
                                </p>
                            </Checkbox>
                        </div>
                        <div className={cx('item')}>
                            <button className={cx('btn')} onClick={() => submitRegister()}>
                                Register
                            </button>
                        </div>
                        <div className={cx('register') + ' mt-5'}>
                            <h4>
                                Already have an account?
                                <Link to="/login">
                                    <span className="ml-3">Login now</span>
                                </Link>
                            </h4>
                        </div>
                    </div>
                </div>
            </div>
            {isLoader && <Loading />}
        </div>
    );
}

export default Register;
