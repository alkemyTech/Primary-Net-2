import NextAuth, { NextAuthOptions } from "next-auth"
import CredentialsProvider from "next-auth/providers/credentials"
import https from 'https';
import { decode } from "jsonwebtoken";


export default NextAuth({

    providers: [
        CredentialsProvider({

            name: "Credentials",
            async authorize(credentials, req) {
                try {
                    const response = await fetch("https://localhost:7149/api/Auth/login", {
                        method: 'POST',
                        body: JSON.stringify({
                            userName: credentials.UserName,
                            password: credentials.Password
                        }),
                        headers: {
                            "Content-Type": "application/json"
                        },
                        agent: new https.Agent({
                            rejectUnauthorized: false
                        })
                    });


                    const data = await response.text();
                    const userResponse = await fetch("https://localhost:7149/api/Auth/me", {
                        method: 'get',
                        headers: {
                            'Authorization': `Bearer ${data}`,
                            "Content-Type": "application/json",

                        },
                        agent: new https.Agent({
                            rejectUnauthorized: false
                        })
                    });

                    const user = await userResponse.json();
                    user.token = data;

                    if (user) {
                        return user;
                    }
                    return null;
                } catch (error) {
                    
                    return null;
                }
            }
        })
    ],
    callbacks: {
        async jwt({ token, user }) {
            return { ...token, ...user };
        },
        async session({ session, token }) {
            if (token) {
                const decodedToken = decode(token.token);
                session.user = token;
                session.expires = decodedToken?.exp;
            }
            return session;
        }
    }
});

