import NextAuth, { NextAuthOptions } from "next-auth"
import CredentialsProvider from "next-auth/providers/credentials"
import https from 'https';
import { decode } from "jsonwebtoken";
import axios from "axios";

export default NextAuth({

  

  providers: [
    CredentialsProvider({
      name: "Credentials",
      async authorize(credentials, req) {
        try {
          const url = "https://localhost:7149/api/Auth/login";
          const agent = new https.Agent({
            rejectUnauthorized: false,
          });

          const response = await axios.post(
            url,
            { userName: credentials.UserName, password: credentials.Password },
            { httpsAgent: agent }
          );

          const headers = {
            Authorization: `Bearer ${response.data}`,
            "Content-Type": "application/json",
          };

          const { data } = await axios.get(
            
            "https://localhost:7149/api/Auth/me",
            {
              headers: headers,
              withCredentials: true,
              httpsAgent: agent,
            }
          );

          const user = { ...data, token: response.data };

          console.log(user);

          if (user) {
            // axios.defaults.headers.common = {"Authorization": `Bearer ${response.data}`}
            return user;
          }
          return null;
        } catch (error) {
          console.error(error);
          return null;
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
